using UnityEngine;

/// <summary>
/// Gerencia o salvamento e o carregamento do progresso do jogador.
/// </summary>
public class SaveManager : MonoBehaviour
{
    public static SaveManager instance; // Instância singleton.

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Garante que apenas uma instância permaneça ativa.
        }
    }

    void Start()
    {
        LoadProgress(); // Carrega o progresso ao iniciar.
    }

    /// <summary>
    /// Salva o progresso atual do jogador.
    /// </summary>
    public static void SaveProgress()
    {
        SaveObject saveObject = new SaveObject();

        saveObject.coins = Inventory.currentCoins; // Salva moedas.
        saveObject.highscore = UIManager.GetHighScore(); // Salva recorde.
        saveObject.shipStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().shipStats; // Salva status da nave.

        SaveLoad.SaveState(saveObject); // Grava os dados.
    }

    /// <summary>
    /// Carrega o progresso salvo do jogador.
    /// </summary>
    public static void LoadProgress()
    {
        SaveObject saveObject = SaveLoad.LoadState();

        Inventory.currentCoins = saveObject.coins; // Restaura moedas.
        UIManager.UpdateHighscore(saveObject.highscore); // Restaura recorde.
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().shipStats = saveObject.shipStats; // Restaura status da nave.
    }
}