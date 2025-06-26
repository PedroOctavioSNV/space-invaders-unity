using UnityEngine;

/// <summary>
/// Controla o escudo do jogador, que perde vida ao ser atingido por projéteis inimigos.
/// </summary>
public class Shield : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] Sprite[] states; // Sprites que representam os estados do escudo.
    SpriteRenderer spriteRenderer;    // Componente para alterar o sprite do escudo.

    int health; // Vida atual do escudo.

    /// <summary>
    /// Inicializa a vida do escudo e obtém o SpriteRenderer.
    /// </summary>
    void Start()
    {
        health = 4;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Trata colisões com projéteis inimigos e amigos.
    /// </summary>
    /// <param name="collision">Informações da colisão.</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            health--;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                spriteRenderer.sprite = states[health - 1];
            }
        }

        if (collision.gameObject.CompareTag("FriendlyBullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}