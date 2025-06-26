using UnityEngine;

/// <summary>
/// Pickup que adiciona saúde ao jogador ao ser coletado.
/// </summary>
public class PickupHealth : Pickup
{
    /// <summary>
    /// Executa a ação de adicionar saúde ao jogador e destrói o pickup.
    /// </summary>
    public override void PickMeUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddHealth();
        Destroy(gameObject);
    }
}