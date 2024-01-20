using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void quitToDesktop()
    {
        Debug.Log("Quitting To Desktop");
        Application.Quit();
    }
}
