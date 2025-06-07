using UnityEngine;

public class Mothership : MonoBehaviour
{
    public int scoreValue;

    private const float MAX_LEFT = -5f;
    private float speed = 5;

    private void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);

        if (transform.position.x <= MAX_LEFT)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}