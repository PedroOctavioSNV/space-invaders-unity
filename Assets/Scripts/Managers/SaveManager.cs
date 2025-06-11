using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadProgress();
    }

    public static void SaveProgress()
    {
        SaveObject saveObject = new SaveObject();

        saveObject.coins = Inventory.currentCoins;
        saveObject.highscore = UIManager.GetHighScore();
        saveObject.shipStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().shipStats = saveObject.shipStats;
    }

    public static void LoadProgress()
    {
        SaveObject saveObject = SaveLoad.LoadState();

        Inventory.currentCoins = saveObject.coins;
        UIManager.UpdateHighscore(saveObject.highscore);


        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().shipStats = saveObject.shipStats;
    }
}