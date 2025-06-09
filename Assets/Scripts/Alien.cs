using UnityEngine;

public class Alien : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField]
    public GameObject explosionPrefab;
    public int scoreValue;

    public void Kill()
    {
        UIManager.UpdateScore(scoreValue);

        AlienMaster.allAliens.Remove(gameObject);

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        if (AlienMaster.allAliens.Count == 0)
        {
            GameManager.SpawNewWave();
        }

        Destroy(gameObject);
    }
}