using UnityEngine;

/// <summary>
/// Pickup que adiciona uma vida ao jogador ao ser coletado.
/// </summary>
public class PickupLife : Pickup
{
    /// <summary>
    /// Executa a a��o de adicionar uma vida ao jogador e destr�i o pickup.
    /// </summary>
    public override void PickMeUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddLife();
        Destroy(gameObject);
    }
}