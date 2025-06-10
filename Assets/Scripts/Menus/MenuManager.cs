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

    public static void showInGameUI()
    {
        instance.mainMenu.SetActive(false);
        instance.pauseMenu.SetActive(false);
        instance.shopMenu.SetActive(false);
        instance.gameOverMenu.SetActive(false);

        instance.inGameUI.SetActive(true);
    }

    public static void OpenMainMenu()
    {
        instance.inGameUI.SetActive(false);

        instance.mainMenu.SetActive(true);
    }

    public static void OpenPause()
    {
        instance.inGameUI.SetActive(false);

        instance.pauseMenu.SetActive(true);
    }

    public static void OpenShop()
    {
        instance.mainMenu.SetActive(false);

        instance.shopMenu.SetActive(true);
    }

    public static void OpenGameOver()
    {
        instance.inGameUI.SetActive(false);

        instance.gameOverMenu.SetActive(true);
    }

    public static void ReturnToMainMenu()
    {
        instance.inGameUI.SetActive(false);
        instance.pauseMenu.SetActive(false);
        instance.shopMenu.SetActive(false);
        instance.gameOverMenu.SetActive(false);

        instance.mainMenu.SetActive(true);
    }

    public static void CloseWindow(GameObject gameObject)
    {

    }
}