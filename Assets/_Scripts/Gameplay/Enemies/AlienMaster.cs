using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla o comportamento coletivo dos aliens, incluindo movimentação, disparo e spawn da nave-mãe.
/// </summary>
public class AlienMaster : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] GameObject bulletPrefab;                           // Prefab do projétil disparado pelos aliens.
    [SerializeField] GameObject mothershipPrefab;                       // Prefab da nave-mãe que aparece ocasionalmente.
    public static List<GameObject> allAliens = new List<GameObject>();  // Lista estática com todos os aliens.

    Vector3 horizontalMoveDistance = new Vector3(0.1f, 0, 0);           // Distância de movimento horizontal.
    Vector3 verticalMoveDistance = new Vector3(0, 0.25f, 0);            // Distância de movimento vertical (descida).
    Vector3 motherShipSpawnPos = new Vector3(3.72f, 4.5f, 0);           // Posição de spawn da nave-mãe.

    const float MAX_LEFT = -3.4f;        // Limite esquerdo da movimentação.
    const float MAX_RIGHT = 3.4f;        // Limite direito da movimentação.
    const float MAX_MOVE_SPEED = 0.02f;  // Velocidade máxima de movimento.
    const float START_Y = 0.35f;         // Posição Y inicial após entrada.
    const float GAMEOVER_Y = -3.5f;      // Posição Y que gera Game Over.

    float moveTimer = 0.01f;             // Timer para controlar o intervalo de movimento.
    const float moveMultiplier = 0.005f; // Multiplicador para ajustar a velocidade conforme aliens restantes.

    float shootTimer = 3f;               // Timer para controlar intervalo de disparo.
    const float initialShootTime = 3f;   // Tempo inicial entre tiros.

    float motherShipTimer = 60f;         // Timer para spawn da nave-mãe.
    const float MOTHERSHIP_MIN = 15f;    // Tempo mínimo para spawn da nave-mãe.
    const float MOTHERSHIP_MAX = 60f;    // Tempo máximo para spawn da nave-mãe.

    bool movingRight;                    // Flag que indica se os aliens estão indo para a direita.
    bool entering = true;                // Flag que indica se os aliens ainda estão entrando na tela.

    /// <summary>
    /// Inicializa a lista de aliens presentes na cena.
    /// </summary>
    void Start()
    {
        allAliens.Clear();    // Limpa lista para garantir que não haja dados antigos.

        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Alien"))
        {
            allAliens.Add(gameObject);   // Adiciona todos os aliens encontrados na cena.
        }
    }

    /// <summary>
    /// Atualiza o estado dos aliens: entrada, movimentação, disparo e spawn da nave-mãe.
    /// </summary>
    void Update()
    {
        if (entering)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 10);  // Aliens descem para posição inicial.

            if (transform.position.y <= START_Y)
            {
                entering = false;    // Entrada finalizada.
            }
        }
        else
        {
            if (allAliens.Count == 0) return;  // Sai se não houver aliens.

            if (moveTimer <= 0)
            {
                MoveEnemies();    // Move os aliens.
            }

            if (shootTimer <= 0)
            {
                Shoot();          // Dispara tiros.
            }

            if (motherShipTimer <= 0)
            {
                SpawnMotherShip(); // Spawn da nave-mãe.
            }

            moveTimer -= Time.deltaTime;    // Atualiza timers.
            shootTimer -= Time.deltaTime;
            motherShipTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Move os aliens horizontalmente e verticalmente ao atingir limites.
    /// Verifica condição de Game Over.
    /// </summary>
    void MoveEnemies()
    {
        int hitMax = 0;      // Contador para quantos aliens atingiram limite horizontal.
        int gameOverY = 0;   // Contador para quantos aliens ultrapassaram limite vertical (game over).

        for (int i = 0; i < allAliens.Count; i++)
        {
            if (movingRight)
            {
                allAliens[i].transform.position += horizontalMoveDistance;  // Move para direita.
            }
            else
            {
                allAliens[i].transform.position -= horizontalMoveDistance;  // Move para esquerda.
            }

            if (allAliens[i].transform.position.x > MAX_RIGHT || allAliens[i].transform.position.x < MAX_LEFT)
            {
                hitMax++;  // Alien atingiu limite horizontal.
            }

            if (allAliens[i].transform.position.y < GAMEOVER_Y)
            {
                gameOverY++;  // Alien abaixo do limite que gera game over.
            }
        }

        if (hitMax > 0)
        {
            for (int i = 0; i < allAliens.Count; i++)
            {
                allAliens[i].transform.position -= verticalMoveDistance;  // Desce a formação.
            }

            movingRight = !movingRight;  // Inverte direção horizontal.
        }

        if (gameOverY > 0)
        {
            MenuManager.OpenGameOver();  // Abre tela de game over.
        }

        moveTimer = GetMoveSpeed();    // Atualiza o timer de movimento baseado na velocidade atual.
    }

    /// <summary>
    /// Calcula a velocidade de movimentação baseada na quantidade de aliens restantes.
    /// </summary>
    /// <returns>Intervalo para próximo movimento.</returns>
    float GetMoveSpeed()
    {
        float f = allAliens.Count * moveMultiplier;   // Calcula intervalo baseado na quantidade.

        if (f < MAX_MOVE_SPEED)
        {
            return MAX_MOVE_SPEED;   // Retorna o máximo para não ficar rápido demais.
        }
        else
        {
            return f;
        }
    }

    /// <summary>
    /// Realiza o disparo de um projétil por um alien aleatório.
    /// </summary>
    void Shoot()
    {
        Vector2 position = allAliens[Random.Range(0, allAliens.Count)].transform.position;  // Posição do alien que vai atirar.

        Instantiate(bulletPrefab, position, Quaternion.identity);  // Cria bala.

        shootTimer = initialShootTime;  // Reseta timer de tiro.
    }

    /// <summary>
    /// Instancia a nave-mãe em uma posição pré-definida.
    /// </summary>
    void SpawnMotherShip()
    {
        Instantiate(mothershipPrefab, motherShipSpawnPos, Quaternion.identity);  // Spawn nave-mãe.

        motherShipTimer = Random.Range(MOTHERSHIP_MIN, MOTHERSHIP_MAX);  // Reseta timer com valor aleatório.
    }
}