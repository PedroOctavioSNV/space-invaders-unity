using UnityEngine;

/// <summary>
/// Representa os atributos estat�sticos da nave do jogador,
/// incluindo sa�de, vidas, velocidade e taxa de disparo.
/// </summary>
[System.Serializable]
public class ShipStats
{
    [HideInInspector]
    public int maxHealth;           // Quantidade m�xima de sa�de que a nave pode ter.

    [HideInInspector]
    public int currentHealth;       // Sa�de atual da nave.

    [HideInInspector]
    public int maxLives = 3;        // Quantidade m�xima de vidas dispon�veis.

    [HideInInspector]
    public int currentLives = 3;    // Quantidade atual de vidas restantes.

    [HideInInspector]
    public float shipSpeed;         // Velocidade de movimenta��o da nave.

    [HideInInspector]
    public float fireRate = 1;      // Taxa de disparo da nave. Valores menores significam tiros mais r�pidos.
}