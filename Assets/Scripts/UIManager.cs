using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI highScoreText;
    [SerializeField]
    private TextMeshProUGUI coinsText;
    [SerializeField]
    private TextMeshProUGUI waveText;
    [SerializeField]
    private Image[] lifeSprites;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Sprite[] healthBars;

    private int score;
    private int highScore;
    private int coins;
    private int wave;

    private Color32 active = new Color(1, 1, 1, 1);
    private Color32 inactive = new Color(1, 1, 1, 0.25f);

    private static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    public static void UpdateLHealthBar(int health)
    {
        instance.healthBar.sprite = instance.healthBars[health];
    }

    public static void UpdateLives(int lives)
    {
        foreach (Image i in instance.lifeSprites)
        {
            i.color = instance.inactive;
        }

        for (int i = 0; i < lives; i++)
        {
            instance.lifeSprites[i].color = instance.active;
        }
    }

    public static void UpdateCoins()
    {
        instance.coinsText.text = Inventory.currentCoins.ToString();
    }

    public static void UpdateScore(int score)
    {
        instance.score += score;
        instance.scoreText.text = instance.score.ToString("000,000");
    }

    public static int GetHighScore()
    {
        return instance.highScore;
    }

    public static void UpdateHighscore(int highScore)
    {
        if (instance.highScore < highScore)
        {
            instance.highScore = highScore;
            instance.highScoreText.text = instance.highScore.ToString("000,000");
        }
    }

    public static void UpdateWave()
    {
        instance.wave++;
        instance.waveText.text = instance.wave.ToString();
    }
}