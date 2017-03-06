using UnityEngine;
using System.Collections;

public class Player
{
    private int playerNumber;

    public int PlayerNumber
    {
        get { return playerNumber; }
    }

    public int TanksDestroyed { get; private set; }


    // Player isn't a monobevaivour, so it cannot rely on initializing in Start.
    // As a result, I've got a constructor for it defined here to take care of initialization.
    public Player(int playerNumber)
    {
        this.playerNumber = playerNumber;
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        TankHealth.TankDestroyed += OnTankDestroyed;
    }


    // This isn't currently getting called. Player isn't a monobehaviour,
    // So it doesn't get notified about things like Start and OnDisable.
    // When I have enough of a game built to the point I have to handle 
    // quiting back to the main menu, etc., I'll have to make sure to 
    // unsubscribe all the players when we are done with them.
    // Otherwise, my static events will remember references to "ghost" players
    // that should no longer exist, and can cause various issues.
    private void UnsubscribeFromEvents()
    {
        TankHealth.TankDestroyed -= OnTankDestroyed;
    }

    private void OnTankDestroyed(Player playerThatDestroyed)
    {
        if (playerThatDestroyed.playerNumber == playerNumber)
            TanksDestroyed++;
    }
}
