using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MovingObject
{
    public int wallDamage = 1;
    public int pointsPerFood = 10;
    public int pointsPerSoda = 20;

    public Text foodText;

    [SerializeField]
    AudioClip moveSound1;
    [SerializeField]
    AudioClip moveSound2;
    [SerializeField]
    AudioClip eatSound1;
    [SerializeField]
    AudioClip eatSound2;
    [SerializeField]
    AudioClip drinkSound1;
    [SerializeField]
    AudioClip drinkSound2;
    [SerializeField]
    AudioClip gameOverSound;

    [SerializeField]
    float restartLevelDelay = 1f;

    Animator animator;
    int food;

	protected override void Start()
    {
        animator = GetComponent<Animator>();

        food = GameManager.instance.playerFoodPoints;

        foodText.text = "Food: " + food;

        base.Start();
	}

    void Update()
    {
        if (!GameManager.instance.isPlayerTurn)
        {
            return;
        }
        else
        {
            int horizontal = 0;
            int vertical = 0;

            horizontal = (int)Input.GetAxisRaw("Horizontal");
            vertical = (int)Input.GetAxisRaw("Vertical");

            if (horizontal != 0)
            {
                vertical = 0;
            }

            if (horizontal != 0 || vertical != 0)
            {
                AttemptMove<Wall>(horizontal, vertical);
            }
        }
    }

    void OnDisable()
    {
        GameManager.instance.playerFoodPoints = food;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Food")
        {
            other.gameObject.SetActive(false);
            food += pointsPerFood;
            SoundManager.instance.RandomizeSFX(eatSound1, eatSound2);
            foodText.text = "+" + pointsPerFood + " Food: " + food;
        }
        else if (other.tag == "Soda")
        {
            other.gameObject.SetActive(false);
            food += pointsPerSoda;
            SoundManager.instance.RandomizeSFX(drinkSound1, drinkSound2);
            foodText.text = "+" + pointsPerSoda + " Food: " + food;
        }
    }

    protected override void AttemptMove<T>(int xDirection, int yDirection)
    {
        food--;
        foodText.text = "Food: " + food;

        base.AttemptMove<T>(xDirection, yDirection);

        RaycastHit2D hit;

        if (Move(xDirection, yDirection, out hit))
        {
            SoundManager.instance.RandomizeSFX(moveSound1, moveSound2);
        }

        CheckIfGameOver();

        GameManager.instance.isPlayerTurn = false;
    }

    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
        animator.SetTrigger("playerChop");
    }

    public void LoseFood(int loss)
    {
        animator.SetTrigger("playerHit");
        food -= loss;
        foodText.text = "-" + loss + " Food: " + food;

        CheckIfGameOver();
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToBattleScene()//<T>(T component)
    {
        Debug.Log("Reached GoToBattleScene");

        //Enemy enemy = component as Enemy;

        SceneManager.LoadScene("testScene", LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("testScene"));
    }

    void OnMouseDown()
    {
        GoToBattleScene();
    }

    void CheckIfGameOver()
    {
        if (food <= 0)
        {
            SoundManager.instance.PlaySingle(gameOverSound);
            SoundManager.instance.musicSource.Stop();

            GameManager.instance.GameOver();
        }
    }
}
