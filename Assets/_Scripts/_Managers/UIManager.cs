using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gerencia a interface do usu�rio, incluindo pontua��o, vidas, moedas, ondas e barras de sa�de.
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] TextMeshProUGUI scoreText;      // Texto da pontua��o atual.
    [SerializeField] TextMeshProUGUI highScoreText;  // Texto do recorde.
    [SerializeField] TextMeshProUGUI coinsText;      // Texto das moedas coletadas.
    [SerializeField] TextMeshProUGUI waveText;       // Texto da onda atual.
    [SerializeField] Image[] lifeSprites;            // Sprites das vidas.
    [SerializeField] Image healthBar;                // Imagem da barra de sa�de.
    [SerializeField] Sprite[] healthBars;            // Sprites para diferentes n�veis da barra de sa�de.

    int score;                                       // Pontua��o atual.
    int highScore;                                   // Recorde atual.
    int wave;                                        // Onda atual.

    Color32 active = new Color(1, 1, 1, 1);          // Cor para vidas ativas.
    Color32 inactive = new Color(1, 1, 1, 0.25f);    // Cor para vidas inativas.

    static UIManager instance;                       // Inst�ncia singleton.

    void Awake()
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

    /// <summary>
    /// Atualiza a barra de sa�de exibida na UI.
    /// </summary>
    public static void UpdateHealthBar(int health)
    {
        instance.healthBar.sprite = instance.healthBars[health];
    }

    /// <summary>
    /// Atualiza as vidas exibidas na UI.
    /// </summary>
    public static void UpdateLives(int lives)
    {
        foreach (Image i in instance.lifeSprites)
        {
            i.color = instance.inactive; // Desativa todas as vidas.
        }

        for (int i = 0; i < lives; i++)
        {
            instance.lifeSprites[i].color = instance.active; // Ativa as vidas restantes.
        }
    }

    /// <summary>
    /// Atualiza a contagem de moedas exibida na UI.
    /// </summary>
    public static void UpdateCoins()
    {
        instance.coinsText.text = Inventory.currentCoins.ToString();
    }

    /// <summary>
    /// Incrementa e atualiza a pontua��o exibida na UI.
    /// </summary>
    public static void UpdateScore(int score)
    {
        instance.score += score;
        instance.scoreText.text = instance.score.ToString("000,000");
    }

    /// <summary>
    /// Retorna o recorde atual.
    /// </summary>
    public static int GetHighScore()
    {
        return instance.highScore;
    }

    /// <summary>
    /// Atualiza o recorde se a nova pontua��o for maior.
    /// </summary>
    public static void UpdateHighscore(int highScore)
    {
        if (instance.highScore < highScore)
        {
            instance.highScore = highScore;
            instance.highScoreText.text = instance.highScore.ToString("000,000");
        }
    }

    /// <summary>
    /// Verifica e salva o recorde se a pontua��o atual for maior.
    /// </summary>
    public static void HighScoreCheck()
    {
        if (instance.highScore < instance.score)
        {
            UpdateHighscore(instance.score);
            SaveManager.SaveProgress();
        }
    }

    /// <summary>
    /// Atualiza a contagem de ondas exibida na UI.
    /// </summary>
    public static void UpdateWave()
    {
        instance.wave++;
        instance.waveText.text = instance.wave.ToString();
    }

    /// <summary>
    /// Reseta a UI para o estado inicial (pontua��o e onda).
    /// </summary>
    public static void ResetUI()
    {
        instance.score = 0;
        instance.wave = 0;
        instance.scoreText.text = instance.score.ToString("000,000");
        instance.waveText.text = instance.wave.ToString();
    }
}