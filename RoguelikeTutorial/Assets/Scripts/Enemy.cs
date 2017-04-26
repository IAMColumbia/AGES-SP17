using UnityEngine;
using System.Collections;
using System;

public class Enemy : MovingObject
{
    [SerializeField]
    int playerDamage;

    [SerializeField]
    AudioClip enemyAttack1;
    [SerializeField]
    AudioClip enemyAttack2;

    Animator animator;
    Transform target;
    bool skipMove;

	protected override void Start()
    {
        GameManager.instance.AddEnemiesToList(this);

        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        base.Start();
	}

    public void MoveEnemy()
    {
        int xDirection = 0;
        int yDirection = 0;

        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
        {
            yDirection = target.position.y > transform.position.y ? 1 : -1;
        }
        else
        {
            xDirection = target.position.x > transform.position.x ? 1 : -1;
        }

        AttemptMove<Player>(xDirection, yDirection);
    }

    protected override void AttemptMove<T>(int xDirection, int yDirection)
    {
        if (skipMove)
        {
            skipMove = false;
        }
        else
        {
            base.AttemptMove<T>(xDirection, yDirection);

            skipMove = true;
        }
    }

    protected override void OnCantMove<T>(T component)
    {
        Player hitPlayer = component as Player;

        animator.SetTrigger("enemyAttack");

        SoundManager.instance.RandomizeSFX(enemyAttack1, enemyAttack2);

        hitPlayer.LoseFood(playerDamage);
    }
}
