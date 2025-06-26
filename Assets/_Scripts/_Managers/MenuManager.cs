using UnityEngine;

/// <summary>
/// Gerencia os diferentes menus do jogo (principal, pausa, loja, game over, etc).
/// </summary>
public class MenuManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] GameObject inGameUI;     // UI durante o jogo
    [SerializeField] GameObject mainMenu;     // Menu principal
    [SerializeField] GameObject pauseMenu;    // Menu de pausa
    [SerializeField] GameObject shopMenu;     // Menu da loja
    [SerializeField] GameObject gameOverMenu; // Menu de game over

    public static MenuManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Garante singleton
        }
    }

    void Start()
    {
        ReturnToMainMenu(); // Começa no menu principal
    }

    /// <summary>
    /// Inicia o jogo e mostra a UI do gameplay.
    /// </summary>
    public void showInGameUI()
    {
        Time.timeScale = 1;
        GameManager.ResetShield();

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.shipStats.currentHealth = player.shipStats.maxHealth;
        player.shipStats.currentLives = player.shipStats.maxLives;

        UIManager.UpdateHealthBar(player.shipStats.currentHealth);
        UIManager.UpdateCoins();

        instance.mainMenu.SetActive(false);
        instance.pauseMenu.SetActive(false);
        instance.shopMenu.SetActive(false);
        instance.gameOverMenu.SetActive(false);

        instance.inGameUI.SetActive(true);

        GameManager.SpawNewWave();
    }

    /// <summary>
    /// Abre o menu principal.
    /// </summary>
    public void OpenMainMenu()
    {
        instance.inGameUI.SetActive(false);
        instance.mainMenu.SetActive(true);
    }

    /// <summary>
    /// Pausa o jogo e mostra o menu de pausa.
    /// </summary>
    public void OpenPause()
    {
        Time.timeScale = 0;
        instance.inGameUI.SetActive(false);
        instance.pauseMenu.SetActive(true);
    }

    /// <summary>
    /// Fecha o menu de pausa e retoma o jogo.
    /// </summary>
    public void ClosePause()
    {
        Time.timeScale = 1;
        instance.pauseMenu.SetActive(false);
        instance.inGameUI.SetActive(true);
    }

    /// <summary>
    /// Abre a loja.
    /// </summary>
    public void OpenShop()
    {
        instance.mainMenu.SetActive(false);
        instance.shopMenu.SetActive(true);
    }

    /// <summary>
    /// Fecha a loja e volta ao menu principal.
    /// </summary>
    public void CloseShop()
    {
        instance.shopMenu.SetActive(false);
        instance.mainMenu.SetActive(true);
    }

    /// <summary>
    /// Abre o menu de game over.
    /// </summary>
    public static void OpenGameOver()
    {
        Time.timeScale = 0;
        UIManager.HighScoreCheck();

        instance.inGameUI.SetActive(false);
        instance.gameOverMenu.SetActive(true);
    }

    /// <summary>
    /// Retorna ao menu principal e reinicia o estado do jogo.
    /// </summary>
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;

        instance.inGameUI.SetActive(false);
        instance.pauseMenu.SetActive(false);
        instance.shopMenu.SetActive(false);
        instance.gameOverMenu.SetActive(false);

        instance.mainMenu.SetActive(true);
        GameManager.CancelGame();
    }

    /// <summary>
    /// Abre o perfil do desenvolvedor no X (antigo Twitter).
    /// </summary>
    public void OpenX()
    {
        Application.OpenURL("https://x.com/pedriiim001");
    }
}