using UnityEngine;

/// <summary>
/// Representa a nave-mãe inimiga que se move para a esquerda e pode ser destruída por tiros do jogador.
/// </summary>
public class Mothership : MonoBehaviour
{
    [SerializeField] int scoreValue; // Pontos ganhos ao destruir a nave-mãe.

    const float MAX_LEFT = -5f;      // Posição limite à esquerda para destruir o objeto.
    float speed = 5;                 // Velocidade de movimento da nave-mãe.

    /// <summary>
    /// Move a nave-mãe para a esquerda e destrói ao sair da tela.
    /// </summary>
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);

        if (transform.position.x <= MAX_LEFT)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Detecta colisão com projéteis amigáveis, atualiza pontuação e destrói objetos envolvidos.
    /// </summary>
    /// <param name="collision">Informações da colisão.</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FriendlyBullet"))
        {
            UIManager.UpdateScore(scoreValue);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}