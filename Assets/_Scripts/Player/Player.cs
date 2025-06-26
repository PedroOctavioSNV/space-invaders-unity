using System.Collections;
using UnityEngine;

/// <summary>
/// Controla o comportamento do jogador, incluindo movimentação, tiro, dano e respawn.
/// </summary>
public class Player : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] public ShipStats shipStats; // Estatísticas da nave do jogador (saúde, velocidade, etc.).
    [SerializeField] GameObject bulletPrefab;    // Prefab do projétil disparado pelo jogador.
    [SerializeField] AudioClip shootSFX;         // Efeito sonoro de disparo.

    Vector2 offScreenPos = new Vector2(0, 20f);  // Posição fora da tela para respawn.
    Vector2 startPos = new Vector2(0, -5f);      // Posição inicial do jogador.

    const float MAX_LEFT = -3.4f;  // Limite esquerdo de movimentação.
    const float MAX_RIGHT = 3.4f;  // Limite direito de movimentação.

    bool isShooting;  // Flag para controlar se o jogador está disparando.
    bool moveLeft;    // Flag para controle de movimento à esquerda via UI.
    bool moveRight;   // Flag para controle de movimento à direita via UI.

    /// <summary>
    /// Inicializa a posição, vidas e saúde do jogador.
    /// </summary>
    void Start()
    {
        shipStats.currentHealth = shipStats.maxHealth;
        shipStats.currentLives = shipStats.maxLives;

        transform.position = startPos;

        UIManager.UpdateHealthBar(shipStats.currentHealth);
        UIManager.UpdateLives(shipStats.currentLives);
    }

    /// <summary>
    /// Controla entrada do jogador para movimento e disparo.
    /// </summary>
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && transform.position.x > MAX_LEFT)
            transform.Translate(Vector2.left * Time.deltaTime * shipStats.shipSpeed);

        if (Input.GetKey(KeyCode.D) && transform.position.x < MAX_RIGHT)
            transform.Translate(Vector2.right * Time.deltaTime * shipStats.shipSpeed);

        if (Input.GetKey(KeyCode.Space) && !isShooting)
            StartCoroutine(Shoot());

        if (moveLeft && transform.position.x > MAX_LEFT)
            transform.Translate(Vector2.left * Time.deltaTime * shipStats.shipSpeed);

        if (moveRight && transform.position.x < MAX_RIGHT)
            transform.Translate(Vector2.right * Time.deltaTime * shipStats.shipSpeed);
    }

    /// <summary>
    /// Aciona movimento para a esquerda via UI.
    /// </summary>
    public void LeftButtonDown() => moveLeft = true;

    /// <summary>
    /// Aciona movimento para a direita via UI.
    /// </summary>
    public void RightButtonDown() => moveRight = true;

    /// <summary>
    /// Para movimento horizontal via UI.
    /// </summary>
    public void DirectionReleased()
    {
        moveLeft = false;
        moveRight = false;
    }

    /// <summary>
    /// Aciona disparo via UI.
    /// </summary>
    public void ShootButton()
    {
        if (!isShooting)
            StartCoroutine(Shoot());
    }

    /// <summary>
    /// Cria uma bala e toca o som de disparo, respeitando a taxa de tiro.
    /// </summary>
    IEnumerator Shoot()
    {
        isShooting = true;
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        AudioManager.PlaySoundEffect(shootSFX);
        yield return new WaitForSeconds(shipStats.fireRate);
        isShooting = false;
    }

    /// <summary>
    /// Reduz a saúde ao receber dano. Lida com mortes e respawn.
    /// </summary>
    void TakeDamage()
    {
        shipStats.currentHealth--;
        UIManager.UpdateHealthBar(shipStats.currentHealth);

        if (shipStats.currentHealth <= 0)
        {
            shipStats.currentLives--;
            UIManager.UpdateLives(shipStats.currentLives);

            if (shipStats.currentLives <= 0)
                SaveManager.SaveProgress(); // Game Over
            else
                StartCoroutine(Respawn());
        }
    }

    /// <summary>
    /// Detecta colisão com projétil inimigo e aplica dano.
    /// </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// Reposiciona o jogador e restaura a saúde após morte.
    /// </summary>
    IEnumerator Respawn()
    {
        transform.position = offScreenPos;
        yield return new WaitForSeconds(2);

        shipStats.currentHealth = shipStats.maxHealth;
        transform.position = startPos;
        UIManager.UpdateHealthBar(shipStats.currentHealth);
    }

    /// <summary>
    /// Aumenta a saúde do jogador ou converte em pontos se já estiver cheia.
    /// </summary>
    public void AddHealth()
    {
        if (shipStats.currentHealth == shipStats.maxHealth)
            UIManager.UpdateScore(250);
        else
        {
            shipStats.currentHealth++;
            UIManager.UpdateHealthBar(shipStats.currentHealth);
        }
    }

    /// <summary>
    /// Aumenta o número de vidas máximas ou converte em pontos.
    /// </summary>
    public void AddLife()
    {
        if (shipStats.currentLives == shipStats.maxLives)
            UIManager.UpdateScore(1000);
        else
        {
            shipStats.maxLives++;
            UIManager.UpdateLives(shipStats.maxLives);
        }
    }
}