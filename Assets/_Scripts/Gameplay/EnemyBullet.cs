using UnityEngine;

/// <summary>
/// Controla o comportamento do projétil inimigo, movendo-o para baixo.
/// </summary>
public class EnemyBullet : MonoBehaviour
{
    float speed = 10; // Velocidade do projétil.

    /// <summary>
    /// Move o projétil para baixo a cada frame.
    /// </summary>
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
    }

    /// <summary>
    /// Detecta colisões com outros objetos (implementação futura).
    /// </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Lógica de colisão a ser implementada.
    }
}