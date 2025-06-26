using UnityEngine;

/// <summary>
/// Automatically destroys this GameObject after a specified number of seconds.
/// </summary>
public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] float seconds; // Time in seconds to wait before destroying this GameObject.

    /// <summary>
    /// Starts the destruction countdown when the script starts.
    /// </summary>
    void Start()
    {
        Destroy(gameObject, seconds); // Schedule destruction of this GameObject after the specified time.
    }
}