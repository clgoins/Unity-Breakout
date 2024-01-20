using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int livesRemaining;
    [SerializeField] private int stageNumber;

    [SerializeField] public GameObject ballTemplate;   // Prefab referring to a ball object for duplication
    [SerializeField] public GameObject blockTemplate;  // Prefab referring to block object for duplication

    [SerializeField] private HUDController hudControl;
    [SerializeField] private Canvas HUD;

    [SerializeField] private LevelGenerator levelGen;

    private List<Powerup> activePowerups;

    void Start()
    {
        // Initialize lives & score
        livesRemaining = 3;
        score = 0;
        
        // Listen for events from Ball and Block
        Block.OnBlockDestroyed += blockDestroyed;
        Ball.OnLostBall += lostBall;

        // Initialize list of active powerups
        activePowerups = new List<Powerup>();

        // Initialize the HUD
        hudControl.updateLives(livesRemaining);
        hudControl.updateScore(score);
        hudControl.updateStage(stageNumber);

        // Begin the first level
        nextStage();
    }


    private void nextStage()
    {
        //Increase score
        score += 1000 * stageNumber;
        hudControl.updateScore(score);

        stageNumber++;
        hudControl.updateStage(stageNumber);
        Vector2 scale;
        Vector2[] positions = levelGen.newLevel(stageNumber, out scale);
        GameObject newBlock;

        for (int i = 0; i < positions.Length; i++)
        {
            newBlock = Instantiate(blockTemplate, positions[i], Quaternion.identity);
            newBlock.transform.localScale = scale;
        }

    }

    public void blockDestroyed(Vector2 position)
    {
        // Event gets called before block object is destroyed, so be sure to subtract 1 from total here
        int remainingBlocks = GameObject.FindGameObjectsWithTag("block").Length - 1;
        score += 100;
        hudControl.updateScore(score);

        if (remainingBlocks <= 0)
            nextStage();
    }

    public void lostBall()
    {
        // Check if there are any balls remaining
        int remainingBalls = GameObject.FindGameObjectsWithTag("ball").Length - 1;

        if (remainingBalls <= 0)
        {
            livesRemaining--;
            hudControl.updateLives(livesRemaining);
            deactivateAllPowerups();
            Instantiate(ballTemplate, Vector3.zero, Quaternion.identity);
        }
    }

    public void addLives(int lives)
    {
        livesRemaining += lives;
        hudControl.updateLives(livesRemaining);
    }

    private void deactivateAllPowerups()
    {
        for (int i = 0; i < activePowerups.Count; i++)
        {
            activePowerups[i].deactivate();
        }

        activePowerups = new List<Powerup>();
    }

    public void addPowerup(Powerup powerup)
    {
        activePowerups.Add(powerup);
    }


    public void addScore(int value)
    {
        score += value;
        hudControl.updateScore(score);
    }
}
