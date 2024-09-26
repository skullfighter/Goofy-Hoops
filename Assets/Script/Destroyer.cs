using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private bool isInSafeZone = false;  // Flag to check if the ball collides with a specific object

    void Start()
    {
        // Start a coroutine to destroy the ball after 5 seconds unless it collides with a specific object
        StartCoroutine(DestroyAfterTime(5f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the ball collides with the specific GameObject (e.g., by tag "SafeZone")
        if (collision.gameObject.CompareTag("SafeZone"))
        {
            isInSafeZone = true;  // Set the flag to true if it collides with the "SafeZone"
            Debug.Log("Ball collided with SafeZone: " + collision.gameObject.name);
        }
    }

    // Coroutine to destroy the ball after a delay unless it has collided with the specific object
    private IEnumerator DestroyAfterTime(float delay)
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delay);

        // If the ball has not collided with the specific GameObject, destroy it
        if (!isInSafeZone)
        {
            Destroy(gameObject);
        }
    }
}
