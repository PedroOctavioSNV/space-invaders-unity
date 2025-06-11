using UnityEngine;

public class Alien : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private GameObject lifePrefab;
    [SerializeField]
    private GameObject healthPrefab;
    [SerializeField]
    private int scoreValue;

    private const int LIFE_CHANCE = 1;
    private const int HEALTH_CHANCE = 10;
    private const int COIN_CHANCE = 50;

    public void Kill()
    {
        UIManager.UpdateScore(scoreValue);

        AlienMaster.allAliens.Remove(gameObject);

        int random = Random.Range(0, 1000);

        if (random == LIFE_CHANCE)
        {
            Instantiate(lifePrefab, transform.position, Quaternion.identity);
        }
        else if (random <= HEALTH_CHANCE)
        {
            Instantiate(healthPrefab, transform.position, Quaternion.identity);
        }
        else if (random <= COIN_CHANCE)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        AudioManager.UpdateBattleMusicDelay(AlienMaster.allAliens.Count);

        if (AlienMaster.allAliens.Count == 0)
        {
            GameManager.SpawNewWave();
        }

        Destroy(gameObject);
    }
}