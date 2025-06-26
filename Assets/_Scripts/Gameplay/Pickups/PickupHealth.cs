using UnityEngine;

/// <summary>
/// Pickup que adiciona sa�de ao jogador ao ser coletado.
/// </summary>
public class PickupHealth : Pickup
{
    /// <summary>
    /// Executa a a��o de adicionar sa�de ao jogador e destr�i o pickup.
    /// </summary>
    public override void PickMeUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddHealth();
        Destroy(gameObject);
    }
}