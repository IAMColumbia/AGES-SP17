using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeathBorder : MonoBehaviour
{
    #region SerializedFields
    [SerializeField]
    private GameObject[] players;
    [SerializeField]
    private GameObject afterActionPanel;
    [SerializeField]
    private Text winText;
    [SerializeField]
    private AudioSource stageMusic;
    #endregion

    private int deadPlayers = 0;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TankDamage playerDamage = other.GetComponent<TankDamage>();
            playerDamage.Explode();
            other.gameObject.SetActive(false);

            deadPlayers++;
            Debug.Log(deadPlayers + " Dead Players");

            if (deadPlayers == (players.Length - 1))
            {
                stageMusic.Stop();
                afterActionPanel.SetActive(true);
                updateWinnerText();
            }
        }
    }

    private void updateWinnerText()
    {
        GameObject winner;

        foreach (GameObject player in players)
        {
            if (player.activeSelf)
            {
                winner = player;
                winText.text = winner.name + " Wins!";
            }
        }
    }
}
