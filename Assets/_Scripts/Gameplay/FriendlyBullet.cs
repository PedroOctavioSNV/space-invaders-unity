using UnityEngine;

/// <summary>
/// Representa o projétil disparado pelo jogador, que se move para cima e interage com inimigos e projéteis inimigos.
/// </summary>
public class FriendlyBullet : MonoBehaviour
{
    float speed = 10; // Velocidade do projétil.

    /// <summary>
    /// Move o projétil para cima a cada frame.
    /// </summary>
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    /// <summary>
    /// Detecta colisões com inimigos e projéteis inimigos, aplicando dano e destruindo o projétil.
    /// </summary>
    /// <param name="collision">Informações da colisão.</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Alien"))
        {
            collision.gameObject.GetComponent<Alien>().Kill();
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}