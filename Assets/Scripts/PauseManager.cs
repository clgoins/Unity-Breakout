
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseManager : MonoBehaviour
{
    [SerializeField] private Canvas pauseScreen;
    private bool gamePaused;
    private GameManager gameManager;

    private void Start()
    {
        gamePaused = false;
        pauseScreen.enabled = false;
        gameManager = GetComponent<GameManager>();
    }

    private void Update()
    {
        
    }


    // Called via player input component
    public void OnPause()
    {
        if (gamePaused)
            resumeGame();
        else
            pauseGame();
    }

    public void pauseGame()
    {
        pauseScreen.enabled = true;
        Time.timeScale = 0;
        gamePaused = true;
    }


    public void resumeGame()
    {
        pauseScreen.enabled = false;
        Time.timeScale = 1;
        gamePaused = false;
    }

    public void quitToMenu()
    {
        gameManager.removeEventListeners();
        SceneManager.LoadScene("Menu");
    }

}
