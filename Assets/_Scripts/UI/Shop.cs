using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the upgrade shop logic for the player, allowing upgrades to health and fire rate.
/// </summary>
public class Shop : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] TextMeshProUGUI currentGold;       // Displays current gold amount.
    [SerializeField] TextMeshProUGUI healthValues;      // Displays current and next health values.
    [SerializeField] TextMeshProUGUI fireRateValues;    // Displays current and next fire rate values.
    [SerializeField] TextMeshProUGUI healthCost;        // Displays cost for health upgrade.
    [SerializeField] TextMeshProUGUI fireRateCost;      // Displays cost for fire rate upgrade.
    [SerializeField] Button healthButton;               // Button to buy health upgrade.
    [SerializeField] Button fireRateButton;             // Button to buy fire rate upgrade.

    int currentMaxHealth;                               // Current max health of the player.
    float currentFireRate;                              // Current fire rate of the player.

    int nextHealthCost;                                 // Cost for the next health upgrade.
    int nextFireRateCost;                               // Cost for the next fire rate upgrade.

    int maxHealthMultiplier = 5;                        // Multiplier for health cost calculation.
    int fireRateMultiplier = 5;                         // Multiplier for fire rate cost calculation.
    int maxHealthBaseCost = 10;                         // Base cost for health upgrades.
    int fireRateBaseCost = 5;                           // Base cost for fire rate upgrades.

    Player player;                                      // Reference to the player.

    /// <summary>
    /// Initializes references and UI at the start of the scene.
    /// </summary>
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        currentMaxHealth = player.shipStats.maxHealth;
        currentFireRate = player.shipStats.fireRate;
        currentGold.text = Inventory.currentCoins + "G";

        UpdateUIAndTotals();
    }

    /// <summary>
    /// Updates UI texts and calculates new upgrade costs and values.
    /// </summary>
    void UpdateUIAndTotals()
    {
        // Health upgrade logic
        if (currentMaxHealth < 5)
        {
            nextHealthCost = currentMaxHealth * (maxHealthBaseCost * maxHealthMultiplier);
            healthValues.text = currentMaxHealth + " => " + (currentMaxHealth + 1);
            healthCost.text = nextHealthCost + "G";
            healthButton.interactable = true;
        }
        else
        {
            healthCost.text = "MAX";
            healthValues.text = currentMaxHealth.ToString();
            healthButton.interactable = false;
        }

        // Fire rate upgrade logic
        if (currentFireRate > 0.2f)
        {
            nextFireRateCost = 0;

            // Calculates total cost based on how many steps away from min fire rate (0.2)
            for (float f = 1; f > 0.2f; f -= 0.1f)
            {
                nextFireRateCost += (fireRateBaseCost * fireRateMultiplier);

                if (f <= currentFireRate)
                {
                    break;
                }
            }

            fireRateValues.text = currentFireRate.ToString("0.00") + " => " + (currentFireRate - 0.1f).ToString("0.00");
            fireRateCost.text = nextFireRateCost + "G";
            fireRateButton.interactable = true;
        }
        else
        {
            fireRateCost.text = "MAX";
            fireRateValues.text = "0.20";
            fireRateButton.interactable = false;
        }
    }

    /// <summary>
    /// Attempts to purchase a health upgrade. Deducts gold and updates stats/UI if successful.
    /// </summary>
    public void BuyHealth()
    {
        if (PriceCheck(nextHealthCost))
        {
            Inventory.currentCoins -= nextHealthCost;
            currentGold.text = Inventory.currentCoins + "G";

            player.shipStats.maxHealth++;
            currentMaxHealth = player.shipStats.maxHealth;

            SaveManager.SaveProgress();
            UpdateUIAndTotals();
        }
    }

    /// <summary>
    /// Attempts to purchase a fire rate upgrade. Deducts gold and updates stats/UI if successful.
    /// </summary>
    public void BuyFireRate()
    {
        if (PriceCheck(nextFireRateCost))
        {
            Inventory.currentCoins -= nextFireRateCost;
            currentGold.text = Inventory.currentCoins + "G";

            player.shipStats.fireRate -= 0.1f;
            currentFireRate = player.shipStats.fireRate;

            SaveManager.SaveProgress();
            UpdateUIAndTotals();
        }
    }

    /// <summary>
    /// Checks if the player has enough coins to purchase an upgrade.
    /// </summary>
    /// <param name="cost">Cost of the upgrade.</param>
    /// <returns>True if player has enough coins, false otherwise.</returns>
    bool PriceCheck(int cost)
    {
        return Inventory.currentCoins >= cost;
    }

#if UNITY_EDITOR
    /// <summary>
    /// Adds 1000 coins to the player (for editor/debugging purposes).
    /// </summary>
    [MenuItem("Cheats/Add Gold")]
    private static void AddGoldCheat()
    {
        Inventory.currentCoins += 1000;
        UIManager.UpdateCoins();
    }
#endif
}