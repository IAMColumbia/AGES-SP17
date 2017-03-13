using UnityEngine;
using System.Collections;
using System;

public delegate void EnemyDiedDelegate();

public class Enemy : MonoBehaviour
{
    //public static event EnemyDiedDelegate OnEnemyDied;

    public static event Action OnEnemyDied; //Action takes place of making your own DelegateClassType

    //public static event Action<string, int, Player, Enemy> OnEnemyDied; //how to add parameters

    //public static event Func<string, int> OnEnemyDied; //Func<parameter, parameter, returnType> FunctName

    public static int NumberOfEnemiesThatHaveDied { get; private set; }

    private void OnMouseDown()
    {
        Die();
    }

    private void Die()
    {
        NumberOfEnemiesThatHaveDied++;
        Destroy(gameObject);

        //double check to make sure there's actually a method for this delegate
        if (OnEnemyDied != null)
            OnEnemyDied.Invoke();

        //newer C#, doesn't work in Unity since Unity is a liiiittle before
        //OnEnemyDied?.Invoke(); //shorthand for the if-statement above (if != null, then run delegate
    }


}
