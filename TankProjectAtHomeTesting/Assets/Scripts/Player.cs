using UnityEngine;
using System.Collections;

public class Player
{
    private int playerNumber;

    public int PlayerNumber
    {
        get { return playerNumber; }
    }

    public int TanksDestroyed { get; set; }

    public Player(int playerNumber)
    {
        this.playerNumber = playerNumber;
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        TankHealth.TankDestroyed += TankDestroyedEventHandler;
    }

    private void UnsubscribeFromEvents()
    {
        TankHealth.TankDestroyed -= TankDestroyedEventHandler;
    }

    private void TankDestroyedEventHandler(Player playerThatDestroyed)
    {
        if (playerThatDestroyed.playerNumber == playerNumber)
            TanksDestroyed++;
    }
}
