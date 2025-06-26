using UnityEngine;

/// <summary>
/// Controla o escudo do jogador, que perde vida ao ser atingido por proj�teis inimigos.
/// </summary>
public class Shield : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] Sprite[] states; // Sprites que representam os estados do escudo.
    SpriteRenderer spriteRenderer;    // Componente para alterar o sprite do escudo.

    int health; // Vida atual do escudo.

    /// <summary>
    /// Inicializa a vida do escudo e obt�m o SpriteRenderer.
    /// </summary>
    void Start()
    {
        health = 4;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Trata colis�es com proj�teis inimigos e amigos.
    /// </summary>
    /// <param name="collision">Informa��es da colis�o.</param>
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