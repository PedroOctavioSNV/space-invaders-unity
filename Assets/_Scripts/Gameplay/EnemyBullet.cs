using UnityEngine;

/// <summary>
/// Controla o comportamento do proj�til inimigo, movendo-o para baixo.
/// </summary>
public class EnemyBullet : MonoBehaviour
{
    float speed = 10; // Velocidade do proj�til.

    /// <summary>
    /// Move o proj�til para baixo a cada frame.
    /// </summary>
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
    }

    /// <summary>
    /// Detecta colis�es com outros objetos (implementa��o futura).
    /// </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        // L�gica de colis�o a ser implementada.
    }
}