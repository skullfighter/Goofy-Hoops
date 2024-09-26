using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text CounterText;

    private int Count = 0;

    private void Start()
    {
        Count = 0;
        UpdateCounterText();
    }

    private void OnTriggerEnter(Collider other)
    {
        Count += 1;
        UpdateCounterText();
    }

    // Method to reset the count
    public void ResetCount()
    {
        Count = 0;
        UpdateCounterText();
    }

    // Method to update the counter text UI
    private void UpdateCounterText()
    {
        CounterText.text = "Count : " + Count;
    }
}
