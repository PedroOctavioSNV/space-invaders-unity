using UnityEngine;

public class FriendlyBullet : MonoBehaviour
{
    private float speed = 10;

    private void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}