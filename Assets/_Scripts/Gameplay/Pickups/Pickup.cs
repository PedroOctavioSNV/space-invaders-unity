using UnityEngine;

/// <summary>
/// Classe base abstrata para pickups que caem e s�o coletados pelo jogador.
/// </summary>
public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float fallSpeed; // Velocidade de queda do pickup.

    /// <summary>
    /// Atualiza a posi��o do pickup para descer constantemente.
    /// </summary>
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * fallSpeed);
    }

    /// <summary>
    /// M�todo abstrato chamado quando o pickup � coletado pelo jogador.
    /// </summary>
    public abstract void PickMeUp();

    /// <summary>
    /// Detecta colis�o com o jogador e executa a a��o de coleta.
    /// </summary>
    /// <param name="collision">Informa��es da colis�o.</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PickMeUp();
        }
    }
}