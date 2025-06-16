using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct PickupDrop
{
    public GameObject prefab;
    [Range(0, 100)]
    public int dropChance; // chance em porcentagem
    public string name; // apenas para organização no Inspector
}

public class Alien : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject explosionPrefab;

    [Header("Drop Settings")]
    [SerializeField] private List<PickupDrop> pickupDrops;

    [Header("Score Settings")]
    [SerializeField] private int scoreValue;

    public void Kill()
    {
        UIManager.UpdateScore(scoreValue);
        AlienMaster.allAliens.Remove(gameObject);

        TryDropPickup();

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        AudioManager.UpdateBattleMusicDelay(AlienMaster.allAliens.Count);

        if (AlienMaster.allAliens.Count == 0)
        {
            GameManager.SpawNewWave();
        }

        Destroy(gameObject);
    }

    private void TryDropPickup()
    {
        int roll = Random.Range(0, 100);

        foreach (var drop in pickupDrops)
        {
            if (roll < drop.dropChance)
            {
                Instantiate(drop.prefab, transform.position, Quaternion.identity);
                return; // drop apenas um item
            }
        }
    }
}