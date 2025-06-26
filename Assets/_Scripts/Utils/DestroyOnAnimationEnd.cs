using UnityEngine;

/// <summary>
/// Destroys the GameObject after the current animation clip finishes playing,
/// with an optional delay added.
/// </summary>
public class DestroyOnAnimationEnd : MonoBehaviour
{
    [SerializeField] float delay; // Delay (seconds) before destruction.

    /// <summary>
    /// Starts destruction timer based on animation length plus delay.
    /// </summary>
    void Start()
    {
        Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay); // Destroy after animation + delay.
    }
}