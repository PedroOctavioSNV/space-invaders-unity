using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField]
    private GameObject bulletPrefab;

    private const float MAX_LEFT = -2.5f;
    private const float MAX_RIGHT = 2.5f;

    private float speed = 3;
    private float cooldown = 0.5f;

    private bool isShooting;

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A) && transform.position.x > MAX_LEFT)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.D) && transform.position.x < MAX_RIGHT)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.Space) && !isShooting)
        {
            StartCoroutine(Shoot());
        }
#endif
    }

    private IEnumerator Shoot()
    {
        isShooting = true;
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(cooldown);
        isShooting = false;
    }
}