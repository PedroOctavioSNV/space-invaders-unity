/// <summary>
/// Pickup que adiciona uma moeda ao invent�rio ao ser coletado.
/// </summary>
public class PickupCoin : Pickup
{
    /// <summary>
    /// Incrementa as moedas do jogador, atualiza a UI e destr�i o pickup.
    /// </summary>
    public override void PickMeUp()
    {
        Inventory.currentCoins++;
        UIManager.UpdateCoins();
        Destroy(gameObject);
    }
}