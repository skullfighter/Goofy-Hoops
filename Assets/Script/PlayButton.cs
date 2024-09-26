using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private Button button;
    private BallThrower ballThrower;

    void Start()
    {
        // Find the BallThrower component from the GameObject named "Game Manager"
        ballThrower = GameObject.Find("Game Manager").GetComponent<BallThrower>();
        button = GetComponent<Button>();
        button.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        Debug.Log("Play button clicked!"); // Log when button is clicked
        if (ballThrower != null)
        {
            Debug.Log("BallThrower found."); // Log if BallThrower is found
            ballThrower.StartGame(); // Call StartGame in BallThrower
        }
        else
        {
            Debug.Log("BallThrower is null! Check if it's assigned correctly."); // Log if BallThrower is null
        }
    }
}

