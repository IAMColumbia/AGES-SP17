using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    Canvas battleCanvas;
    [SerializeField]
    float pauseLengthBetweenTurns;

    Stats knight;
    NecroAttacks enemy;

    bool isPlayerTurn = true;

    void Start()
    {
        knight = GameObject.Find("Knight").GetComponent<Stats>();
        enemy = GameObject.Find("Necro").GetComponent<NecroAttacks>();
    }

    void EnemyTurn()
    {
        enemy.ChooseAttack();
    }

    public void EndTurn()
    {
        isPlayerTurn = !isPlayerTurn;

        StartCoroutine(PauseBetweenTurns());
    }

    public void EndBattle()
    {
        if (knight.Strength > 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
        else
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }

    IEnumerator PauseBetweenTurns()
    {
        if (isPlayerTurn)
        {
            yield return new WaitForSeconds(pauseLengthBetweenTurns);

            battleCanvas.enabled = true;
        }
        else
        {
            battleCanvas.enabled = false;

            yield return new WaitForSeconds(pauseLengthBetweenTurns);

            EnemyTurn();
        }
    }
}
