using UnityEngine;

/// <summary>
/// Classe base abstrata para pickups que caem e são coletados pelo jogador.
/// </summary>
public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float fallSpeed; // Velocidade de queda do pickup.

    /// <summary>
    /// Atualiza a posição do pickup para descer constantemente.
    /// </summary>
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * fallSpeed);
    }

    /// <summary>
    /// Método abstrato chamado quando o pickup é coletado pelo jogador.
    /// </summary>
    public abstract void PickMeUp();

    /// <summary>
    /// Detecta colisão com o jogador e executa a ação de coleta.
    /// </summary>
    /// <param name="collision">Informações da colisão.</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PickMeUp();
        }
    }
}