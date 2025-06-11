using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField]
    public ShipStats shipStats;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private AudioClip shootSFX;

    private Vector2 offScreenPos = new Vector2(0, 20f);
    private Vector2 startPos = new Vector2(0, -4f);

    private const float MAX_LEFT = -3.4f;
    private const float MAX_RIGHT = 3.4f;

    private bool isShooting;

    private bool moveLeft;
    private bool moveRight;

    private void Start()
    {
        shipStats.currentHealth = shipStats.maxHealth;
        shipStats.currentLives = shipStats.maxLives;

        transform.position = startPos;

        UIManager.UpdateLHealthBar(shipStats.currentHealth);
        UIManager.UpdateLives(shipStats.currentLives);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A) && transform.position.x > MAX_LEFT)
        {
            transform.Translate(Vector2.left * Time.deltaTime * shipStats.shipSpeed);
        }

        if (Input.GetKey(KeyCode.D) && transform.position.x < MAX_RIGHT)
        {
            transform.Translate(Vector2.right * Time.deltaTime * shipStats.shipSpeed);
        }

        if (Input.GetKey(KeyCode.Space) && !isShooting)
        {
            StartCoroutine(Shoot());
        }
#endif

        if (moveLeft && transform.position.x > MAX_LEFT)
        {
            transform.Translate(Vector2.left * Time.deltaTime * shipStats.shipSpeed);
        }

        if (moveRight && transform.position.x < MAX_RIGHT)
        {
            transform.Translate(Vector2.right * Time.deltaTime * shipStats.shipSpeed);
        }
    }

    public void LeftButtonDown()
    {
        moveLeft = true;
    }

    public void RightButtonDown()
    {
        moveRight = true;
    }

    public void DirectionReleased()
    {
        moveLeft = false;
        moveRight = false;
    }

    public void ShootButton()
    {
        if (!isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        isShooting = true;
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        AudioManager.PlaySoundEffect(shootSFX);

        yield return new WaitForSeconds(shipStats.fireRate);
        isShooting = false;
    }

    private void TakeDamage()
    {
        shipStats.currentHealth--;
        UIManager.UpdateLHealthBar(shipStats.currentHealth);

        if (shipStats.currentHealth <= 0)
        {
            shipStats.currentLives--;
            UIManager.UpdateLives(shipStats.currentLives);

            if (shipStats.currentLives <= 0)
            {
                // Game Over
                SaveManager.SaveProgress();
            }
            else
            {
                // Respaw Player
                StartCoroutine(Respawn());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator Respawn()
    {
        transform.position = offScreenPos;
        yield return new WaitForSeconds(2);

        shipStats.currentHealth = shipStats.maxHealth;
        transform.position = startPos;
        UIManager.UpdateLHealthBar(shipStats.currentHealth);
    }

    public void AddHealth()
    {
        if (shipStats.currentHealth == shipStats.maxHealth)
        {
            UIManager.UpdateScore(250);
        }
        else
        {
            shipStats.currentHealth++;
            UIManager.UpdateLHealthBar(shipStats.currentHealth);
        }
    }

    public void AddLife()
    {
        if (shipStats.currentLives == shipStats.maxLives)
        {
            UIManager.UpdateScore(1000);
        }
        else
        {
            shipStats.maxLives++;
            UIManager.UpdateLives(shipStats.maxLives);
        }
    }
}