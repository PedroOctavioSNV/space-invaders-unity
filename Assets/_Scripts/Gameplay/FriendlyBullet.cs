using UnityEngine;

/// <summary>
/// Representa o proj�til disparado pelo jogador, que se move para cima e interage com inimigos e proj�teis inimigos.
/// </summary>
public class FriendlyBullet : MonoBehaviour
{
    float speed = 10; // Velocidade do proj�til.

    /// <summary>
    /// Move o proj�til para cima a cada frame.
    /// </summary>
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    /// <summary>
    /// Detecta colis�es com inimigos e proj�teis inimigos, aplicando dano e destruindo o proj�til.
    /// </summary>
    /// <param name="collision">Informa��es da colis�o.</param>
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