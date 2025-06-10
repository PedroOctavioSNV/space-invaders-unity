[System.Serializable]

public class SaveObject
{
    public int coins;
    public int highscore;
    public ShipStats shipStats;

    public SaveObject()
    {
        coins = 0;
        highscore = 0;

        shipStats = new ShipStats();
        shipStats.maxHealth = 5;
        shipStats.maxLives = 3;
        shipStats.shipSpeed = 3;
        shipStats.fireRate = 0.5f;
    }
}