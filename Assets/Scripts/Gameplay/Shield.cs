using UnityEngine;

public class Shield : MonoBehaviour
{
    public Sprite[] states;

    private int health;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        health = 4;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            health--;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                spriteRenderer.sprite = states[health - 1];
            }
        }

        if (collision.gameObject.CompareTag("FriendlyBullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}