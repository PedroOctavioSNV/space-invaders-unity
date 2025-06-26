using UnityEngine;

/// <summary>
/// Representa a nave-m�e inimiga que se move para a esquerda e pode ser destru�da por tiros do jogador.
/// </summary>
public class Mothership : MonoBehaviour
{
    [SerializeField] int scoreValue; // Pontos ganhos ao destruir a nave-m�e.

    const float MAX_LEFT = -5f;      // Posi��o limite � esquerda para destruir o objeto.
    float speed = 5;                 // Velocidade de movimento da nave-m�e.

    /// <summary>
    /// Move a nave-m�e para a esquerda e destr�i ao sair da tela.
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
    /// Detecta colis�o com proj�teis amig�veis, atualiza pontua��o e destr�i objetos envolvidos.
    /// </summary>
    /// <param name="collision">Informa��es da colis�o.</param>
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