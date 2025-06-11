using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inGameUI;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject shopMenu;
    [SerializeField]
    private GameObject gameOverMenu;

    public static MenuManager instance;

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
        ReturnToMainMenu();
    }

    public void showInGameUI()
    {
        Time.timeScale = 1;

        instance.mainMenu.SetActive(false);
        instance.pauseMenu.SetActive(false);
        instance.shopMenu.SetActive(false);
        instance.gameOverMenu.SetActive(false);

        instance.inGameUI.SetActive(true);

        GameManager.SpawNewWave();
    }

    public void OpenMainMenu()
    {
        instance.inGameUI.SetActive(false);

        instance.mainMenu.SetActive(true);
    }

    public void OpenPause()
    {
        Time.timeScale = 0;

        instance.inGameUI.SetActive(false);

        instance.pauseMenu.SetActive(true);
    }

    public void ClosePause()
    {
        Time.timeScale = 1;

        instance.pauseMenu.SetActive(false);

        instance.inGameUI.SetActive(true);
    }

    public void OpenShop()
    {
        instance.mainMenu.SetActive(false);

        instance.shopMenu.SetActive(true);
    }

    public void CloseShop()
    {
        instance.shopMenu.SetActive(false);

        instance.mainMenu.SetActive(true);
    }

    public static void OpenGameOver()
    {
        instance.inGameUI.SetActive(false);

        instance.gameOverMenu.SetActive(true);
    }

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
}