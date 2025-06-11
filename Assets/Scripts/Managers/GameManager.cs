using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private GameObject currentSet;
    private Vector2 spawnPosition = new Vector2(0, 10);

    [SerializeField]
    private GameObject[] allAlienSets;

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

    public static void SpawNewWave()
    {
        instance.StartCoroutine(instance.SpawnWave());
    }

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

    private IEnumerator SpawnWave()
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
}