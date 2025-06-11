using UnityEditor;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TextMeshProUGUI currentGold;
    public TextMeshProUGUI healthValues;
    public TextMeshProUGUI fireRateValues;
    public TextMeshProUGUI healthCost;
    public TextMeshProUGUI fireRateCost;

    public Button healthButton;
    public Button fireRateButton;

    private int currentMaxHealth;
    private float currentFireRate;

    private int nextHealthCost;
    private int nextFireRateCost;

    private int maxHealthMultiplier = 5;
    private int fireRateMultiplier = 5;

    private int maxHealthBaseCost = 10;
    private int fireRateBaseCost = 5;

    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        currentMaxHealth = player.shipStats.maxHealth;
        currentFireRate = player.shipStats.fireRate;
        currentGold.text = Inventory.currentCoins + "G";

        UpdateUIAndTotals();
    }

    private void UpdateUIAndTotals()
    {
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

        if (currentFireRate > 0.2f)
        {
            nextFireRateCost = 0;

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

    private bool PriceCheck(int cost)
    {
        if (Inventory.currentCoins >= cost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

#if UNITY_EDITOR
    [MenuItem("Cheats/Add Gold")]
    private static void AddGoldCheat()
    {
        Inventory.currentCoins += 1000;
        UIManager.UpdateCoins();
    }
#endif
}