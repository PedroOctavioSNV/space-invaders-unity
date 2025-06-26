using UnityEngine;

/// <summary>
/// Pickup que adiciona uma vida ao jogador ao ser coletado.
/// </summary>
public class PickupLife : Pickup
{
    /// <summary>
    /// Executa a ação de adicionar uma vida ao jogador e destrói o pickup.
    /// </summary>
    public override void PickMeUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddLife();
        Destroy(gameObject);
    }
}