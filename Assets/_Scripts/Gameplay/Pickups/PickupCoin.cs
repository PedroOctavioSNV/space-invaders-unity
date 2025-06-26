/// <summary>
/// Pickup que adiciona uma moeda ao inventário ao ser coletado.
/// </summary>
public class PickupCoin : Pickup
{
    /// <summary>
    /// Incrementa as moedas do jogador, atualiza a UI e destrói o pickup.
    /// </summary>
    public override void PickMeUp()
    {
        Inventory.currentCoins++;
        UIManager.UpdateCoins();
        Destroy(gameObject);
    }
}