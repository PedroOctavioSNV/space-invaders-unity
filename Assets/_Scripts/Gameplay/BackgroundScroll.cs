using UnityEngine;

/// <summary>
/// Controla o movimento de rolagem contínua do fundo.
/// </summary>
public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] float scrollSpeed; // Velocidade da rolagem do fundo.

    new Renderer renderer; // Renderer do objeto para manipular o material.

    /// <summary>
    /// Inicializa o renderer no início.
    /// </summary>
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    /// <summary>
    /// Atualiza o offset da textura para criar o efeito de rolagem.
    /// </summary>
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}