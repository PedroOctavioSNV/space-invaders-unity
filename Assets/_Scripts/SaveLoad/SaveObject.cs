using System;

/// <summary>
/// Represents the structure of the game save data.
/// </summary>
[Serializable]
public class SaveObject
{
    public int coins;           // Total number of coins the player has.
    public int highscore;       // The highest score achieved by the player.
    public ShipStats shipStats; // Stats associated with the player's ship.

    /// <summary>
    /// Initializes a new instance of the <see cref="SaveObject"/> class with default values.
    /// </summary>
    public SaveObject()
    {
        coins = 0;
        highscore = 0;

        shipStats = new ShipStats
        {
            maxHealth = 1,
            maxLives = 3,
            shipSpeed = 3,
            fireRate = 1f
        };
    }
}