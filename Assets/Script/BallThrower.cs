/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallThrower : MonoBehaviour
{
    public GameObject ballPrefab;         // The ball prefab to instantiate
    public float yForce = 10f;            // Constant force applied in the Y direction
    public float xForceMultiplier = 0.1f; // Multiplier for the drag distance to calculate X force

    private GameObject currentBall;       // Reference to the currently instantiated ball
    private Rigidbody ballRb;             // Rigidbody of the ball
    private Vector3 dragStartPos;
    public Text Timer;                    // Start position of the drag
    public Text gameOverText;             // UI Text component to display the Game Over message
    public Button restartButton;          // Button for restarting the game
    public Button playButton;             // Button for starting the game
    public float currCountdownValue;      // Countdown value for timer

    bool isGameActive = false;            // Tracks if the game is active

    // Method that starts the game, called from PlayButton.cs
    public void StartGame()
    {
        Debug.Log("StartGame called"); // Add this line
        isGameActive = true;
        gameOverText.gameObject.SetActive(false); // Hide Game Over text
        StartCoroutine(StartCountdown()); // Start countdown timer
        playButton.gameObject.SetActive(false);  // Hide play button after game starts
    }


    void Update()
    {
        // Only allow input if the game is active
        if (!isGameActive)
            return;

        if (Input.GetMouseButtonDown(0))  // On mouse or touch press
        {
            // Instantiate the ball at the point of touch/click
            Vector3 spawnPosition = GetMouseWorldPosition();
            currentBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
            ballRb = currentBall.GetComponent<Rigidbody>();
            ballRb.isKinematic = true;  // Make the ball static while dragging

            // Store the initial drag position
            dragStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && currentBall != null)  // While the mouse is held down
        {
            // Update the ball's position based on mouse movement (dragging)
            Vector3 currentMousePosition = GetMouseWorldPosition();
            currentBall.transform.position = currentMousePosition;
        }

        if (Input.GetMouseButtonUp(0) && currentBall != null)  // On mouse or touch release
        {
            // Capture the release position
            Vector3 dragEndPos = Input.mousePosition;

            // Calculate the drag distance
            Vector3 dragVector = dragEndPos - dragStartPos;

            // Calculate forces based on drag
            float xForce = -dragVector.y * xForceMultiplier; // Negative X force based on vertical drag
            float yForceToApply = yForce; // Constant Y force

            // Create a force vector with constant Y force and calculated negative X force
            Vector3 forceToApply = new Vector3(xForce, yForceToApply, 0); // No force in Z direction

            ballRb.isKinematic = false;  // Enable physics on the ball
            ballRb.AddForce(forceToApply, ForceMode.Impulse); // Apply the calculated force

            // Clear the reference after the throw
            currentBall = null;
        }
    }

    // Utility function to get the world position from mouse/touch input
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane + 10f;  // Adjust this value as per your scene setup
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    // Countdown timer coroutine
    public IEnumerator StartCountdown(float countdownValue = 60)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Timer.text = "Timer: " + currCountdownValue;
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        if (currCountdownValue == 0)
        {
            GameOver();
        }
    }

    // Game over method
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        playButton.gameObject.SetActive(true);
    }

    // Restart method
    plic void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallThrower : MonoBehaviour
{
    public GameObject ballPrefab;         // The ball prefab to instantiate
    public float yForce = 10f;            // Constant force applied in the Y direction
    public float xForceMultiplier = 0.1f; // Multiplier for the drag distance to calculate X force

    private GameObject currentBall;       // Reference to the currently instantiated ball
    private Rigidbody ballRb;             // Rigidbody of the ball
    private Vector3 dragStartPos;
    public Text Timer;                    // Timer UI Text
    public Text gameOverText;             // UI Text component to display the Game Over message
    public Button restartButton;          // Button for restarting the game
    public Button playButton;             // Button for starting the game
    public float currCountdownValue;      // Countdown value for timer

    private bool isGameActive = false;    // Tracks if the game is active
    private List<GameObject> instantiatedBalls = new List<GameObject>(); // List to keep track of instantiated balls

    // Reference to the Counter script
    public Counter counter; // Drag your Counter GameObject here in the Inspector

    // Method that starts the game, called from PlayButton.cs
    public void StartGame()
    {
        Debug.Log("StartGame called"); // Log for debugging
        isGameActive = true;
        gameOverText.gameObject.SetActive(false); // Hide Game Over text
        StartCoroutine(StartCountdown()); // Start countdown timer
        playButton.gameObject.SetActive(false);  // Hide play button after game starts

        // Reset the counter when the game starts
        counter.ResetCount(); // Reset count in Counter script
    }

    void Update()
    {
        // Only allow input if the game is active
        if (!isGameActive)
            return;

        if (Input.GetMouseButtonDown(0))  // On mouse or touch press
        {
            // Instantiate the ball at the point of touch/click
            Vector3 spawnPosition = GetMouseWorldPosition();
            currentBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
            instantiatedBalls.Add(currentBall); // Add the instantiated ball to the list
            ballRb = currentBall.GetComponent<Rigidbody>();
            ballRb.isKinematic = true;  // Make the ball static while dragging

            // Store the initial drag position
            dragStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && currentBall != null)  // While the mouse is held down
        {
            // Update the ball's position based on mouse movement (dragging)
            Vector3 currentMousePosition = GetMouseWorldPosition();
            currentBall.transform.position = currentMousePosition;
        }

        if (Input.GetMouseButtonUp(0) && currentBall != null)  // On mouse or touch release
        {
            // Capture the release position
            Vector3 dragEndPos = Input.mousePosition;

            // Calculate the drag distance
            Vector3 dragVector = dragEndPos - dragStartPos;

            // Calculate forces based on drag
            float xForce = -dragVector.y * xForceMultiplier; // Negative X force based on vertical drag
            float yForceToApply = yForce; // Constant Y force

            // Create a force vector with constant Y force and calculated negative X force
            Vector3 forceToApply = new Vector3(xForce, yForceToApply, 0); // No force in Z direction

            ballRb.isKinematic = false;  // Enable physics on the ball
            ballRb.AddForce(forceToApply, ForceMode.Impulse); // Apply the calculated force

            // Clear the reference after the throw
            currentBall = null;
        }
    }

    // Utility function to get the world position from mouse/touch input
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane + 10f;  // Adjust this value as per your scene setup
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    // Countdown timer coroutine
    public IEnumerator StartCountdown(float countdownValue = 60)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Timer.text = "Timer: " + currCountdownValue;
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        if (currCountdownValue == 0)
        {
            GameOver();
        }
    }

    // Game over method
    public void GameOver()
    {
        // Destroy all instantiated balls
        foreach (GameObject ball in instantiatedBalls)
        {
            Destroy(ball);
        }
        instantiatedBalls.Clear(); // Clear the list after destroying

        // Reset the counter
        counter.ResetCount(); // Reset count in Counter script

        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        playButton.gameObject.SetActive(true);
    }

    // Restart method
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
