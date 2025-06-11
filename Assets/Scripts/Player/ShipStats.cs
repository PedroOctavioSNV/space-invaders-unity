using UnityEngine;

[System.Serializable]
public class ShipStats
{
    [Range(1, 5)]
    [HideInInspector]
    public int maxHealth;
    [HideInInspector]
    public int currentHealth;
    [HideInInspector]
    public int maxLives = 3;
    [HideInInspector]
    public int currentLives = 3;

    [HideInInspector]
    public float shipSpeed;
    [HideInInspector]
    public float fireRate = 1;
}