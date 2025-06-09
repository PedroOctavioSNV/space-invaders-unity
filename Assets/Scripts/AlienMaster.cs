using System.Collections.Generic;
using UnityEngine;

public class AlienMaster : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField]
    private GameObject bulletPrefab;
    public static List<GameObject> allAliens = new List<GameObject>();
    [SerializeField]
    private GameObject mothershipPrefab;

    private Vector3 horizontalMoveDistance = new Vector3(0.05f, 0, 0);
    private Vector3 verticalMoveDistance = new Vector3(0, 0.15f, 0);
    private Vector3 motherShipSpawnPos = new Vector3(3.72f, 3.45f, 0);

    private const float MAX_LEFT = -3.4f;
    private const float MAX_RIGHT = 3.4f;
    private const float MAX_MOVE_SPEED = 0.02f;
    private const float START_Y = 0.35f;

    private float moveTimer = 0.01f;
    private const float moveMultiplier = 0.005f;

    private float shootTimer = 3f;
    private const float initialShootTime = 3f;

    private float motherShipTimer = 60f;
    private const float MOTHERSHIP_MIN = 15f;
    private const float MOTHERSHIP_MAX = 60f;

    private bool movingRight;
    private bool entering = true;

    private void Start()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Alien"))
        {
            allAliens.Add(gameObject);
        }
    }

    private void Update()
    {

        if (entering)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 10);

            if (transform.position.y <= START_Y)
            {
                entering = false;

            }
        }
        else
        {
            if (moveTimer <= 0)
            {
                MoveEnemies();
            }

            if (shootTimer <= 0)
            {
                Shoot();
            }

            if (motherShipTimer <= 0)
            {
                SpawnMotherShip();
            }

            moveTimer -= Time.deltaTime;
            shootTimer -= Time.deltaTime;
            motherShipTimer -= Time.deltaTime;
        }
    }

    private void MoveEnemies()
    {
        if (allAliens.Count > 0)
        {

            int hitMax = 0;

            for (int i = 0; i < allAliens.Count; i++)
            {
                if (movingRight)
                {
                    allAliens[i].transform.position += horizontalMoveDistance;
                }
                else
                {
                    allAliens[i].transform.position -= horizontalMoveDistance;
                }

                if (allAliens[i].transform.position.x > MAX_RIGHT || allAliens[i].transform.position.x < MAX_LEFT)
                {
                    hitMax++;
                }
            }

            if (hitMax > 0)
            {
                for (int i = 0; i < allAliens.Count; i++)
                {
                    allAliens[i].transform.position -= verticalMoveDistance;
                }

                movingRight = !movingRight;
            }

            moveTimer = GetMoveSpeed();
        }
    }

    private float GetMoveSpeed()
    {
        float f = allAliens.Count * moveMultiplier;

        if (f < MAX_MOVE_SPEED)
        {
            return MAX_MOVE_SPEED;
        }
        else
        {
            return f;
        }
    }

    private void Shoot()
    {
        Vector2 position = allAliens[Random.Range(0, allAliens.Count)].transform.position;

        Instantiate(bulletPrefab, position, Quaternion.identity);

        shootTimer = initialShootTime;
    }

    private void SpawnMotherShip()
    {
        Instantiate(mothershipPrefab, motherShipSpawnPos, Quaternion.identity);

        motherShipTimer = Random.Range(MOTHERSHIP_MIN, MOTHERSHIP_MAX);
    }
}