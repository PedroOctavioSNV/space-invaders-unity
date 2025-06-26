using UnityEngine;

/// <summary>
/// Representa os atributos estatísticos da nave do jogador,
/// incluindo saúde, vidas, velocidade e taxa de disparo.
/// </summary>
[System.Serializable]
public class ShipStats
{
    [HideInInspector]
    public int maxHealth;           // Quantidade máxima de saúde que a nave pode ter.

    [HideInInspector]
    public int currentHealth;       // Saúde atual da nave.

    [HideInInspector]
    public int maxLives = 3;        // Quantidade máxima de vidas disponíveis.

    [HideInInspector]
    public int currentLives = 3;    // Quantidade atual de vidas restantes.

    [HideInInspector]
    public float shipSpeed;         // Velocidade de movimentação da nave.

    [HideInInspector]
    public float fireRate = 1;      // Taxa de disparo da nave. Valores menores significam tiros mais rápidos.
}