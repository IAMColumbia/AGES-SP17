using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        if (knight.Strength > 0)
        {
            isPlayerTurn = !isPlayerTurn;

            StartCoroutine(PauseBetweenTurns());
        }
    }

    public IEnumerator EndBattle()
    {
        yield return new WaitForSeconds(7.0f);

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
