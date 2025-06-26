using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PickupDrop
{
    [Header("Refs")]
    public GameObject prefab;                       // Prefab do pickup para dropar.

    [SerializeField] string name;                   // Apenas para organização no Inspector.
    [Range(0, 100)]
    public int dropChance;                          // Chance de drop em porcentagem.
}

/// <summary>
/// Comportamento individual do alien, incluindo morte, pontuação e drops.
/// </summary>
public class Alien : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] GameObject explosionPrefab;   // Prefab da explosão ao morrer.

    [Header("Drop Settings")]
    [SerializeField] List<PickupDrop> pickupDrops; // Lista de pickups que podem ser dropados.

    [Header("Score Settings")]
    [SerializeField] int scoreValue;               // Pontos dados ao jogador ao matar o alien.

    /// <summary>
    /// Mata o alien, atualiza pontuação, tenta dropar item, toca explosão, atualiza música e checa nova onda.
    /// </summary>
    public void Kill()
    {
        UIManager.UpdateScore(scoreValue);                   // Atualiza a pontuação.
        AlienMaster.allAliens.Remove(gameObject);            // Remove alien da lista.

        TryDropPickup();                                     // Tenta dropar algum pickup.

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);  // Instancia explosão.

        AudioManager.UpdateBattleMusicDelay(AlienMaster.allAliens.Count);       // Ajusta música da batalha.

        if (AlienMaster.allAliens.Count == 0)
        {
            GameManager.SpawNewWave();                         // Spawna nova onda se não houver aliens.
        }

        Destroy(gameObject);                                   // Destroi o gameObject do alien.
    }

    /// <summary>
    /// Tenta dropar um pickup com base na chance configurada.
    /// </summary>
    void TryDropPickup()
    {
        int roll = Random.Range(0, 100);                       // Sorteia um número para drop chance.

        foreach (var drop in pickupDrops)
        {
            if (roll < drop.dropChance)
            {
                Instantiate(drop.prefab, transform.position, Quaternion.identity); // Instancia pickup.
                return;  // Dropa apenas um item.
            }
        }
    }
}