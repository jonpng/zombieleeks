using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameStarter : MonoBehaviour
{
    public string currentLevel;

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentLevel);
    }
}

