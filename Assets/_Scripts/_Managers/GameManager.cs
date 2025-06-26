using System.Collections;
using UnityEngine;

/// <summary>
/// Gerencia o fluxo principal do jogo, incluindo ondas de inimigos e escudos.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] GameObject[] allAlienSets; // conjuntos de aliens para spawn

    GameObject currentSet;
    Vector2 spawnPosition = new Vector2(0, 10);

    [SerializeField] GameObject shieldPrefab; // prefab do escudo
    Vector2 middleShieldPos = new Vector2(0, -3);
    Vector2 rightShieldPos = new Vector2(2.25f, -3);
    Vector2 leftShieldPos = new Vector2(-2.25f, -3);

    static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // garante singleton
        }
    }

    /// <summary>
    /// Inicia o spawn de uma nova onda de inimigos.
    /// </summary>
    public static void SpawNewWave()
    {
        instance.StartCoroutine(instance.SpawnWave());
    }

    /// <summary>
    /// Cancela o jogo atual, limpando inimigos e resetando o UI.
    /// </summary>
    public static void CancelGame()
    {
        instance.StopAllCoroutines();
        AlienMaster.allAliens.Clear();

        if (instance.currentSet != null)
        {
            Destroy(instance.currentSet);
        }

        UIManager.ResetUI();
        AudioManager.StopBattleMusic();
    }

    IEnumerator SpawnWave()
    {
        AudioManager.UpdateBattleMusicDelay(1);
        AudioManager.StopBattleMusic();
        AlienMaster.allAliens.Clear();

        if (currentSet != null)
        {
            Destroy(currentSet);
        }

        yield return new WaitForSeconds(3);

        currentSet = Instantiate(allAlienSets[Random.Range(0, allAlienSets.Length)], spawnPosition, Quaternion.identity);
        UIManager.UpdateWave();
        AudioManager.PlayBattleMusic();
    }

    /// <summary>
    /// Remove todos os escudos atuais e instancia novos.
    /// </summary>
    public static void ResetShield()
    {
        GameObject[] currentShields = GameObject.FindGameObjectsWithTag("Shield");

        foreach (GameObject shield in currentShields)
        {
            Destroy(shield);
        }

        Instantiate(instance.shieldPrefab, instance.middleShieldPos, Quaternion.identity);
        Instantiate(instance.shieldPrefab, instance.rightShieldPos, Quaternion.identity);
        Instantiate(instance.shieldPrefab, instance.leftShieldPos, Quaternion.identity);
    }
}