using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthManager : MonoBehaviour
{
    public static int health;        // The player's health.


    Text text;                      // Reference to the Text component.


    void Awake()
    {
        // Set up the reference.
        text = GetComponent<Text>();

        // Reset the score.
        health = 400;
    }


    void Update()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        text.text = health.ToString();
    }
}