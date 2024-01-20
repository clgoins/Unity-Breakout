using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameOverController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI stageText;

    private void Start()
    {
        canvas.enabled = false;
    }

    public void newGame()
    {
        SceneManager.LoadScene("game");
    }

    public void quit()
    {
        SceneManager.LoadScene("menu");
    }

    public void gameOver(int score, int stage)
    {
        canvas.enabled = true;
        Time.timeScale = 0;
        scoreText.SetText("" + score);
        stageText.SetText("" + stage);
    }
}
