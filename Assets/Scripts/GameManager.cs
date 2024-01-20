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
    [SerializeField] private GameOverController gameOverCtrl;

    [SerializeField] private LevelGenerator levelGen;

    private bool waitingForKey = false;

    private List<Powerup> activePowerups;


    // Runs when object is started before first update frame
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
        HUD.enabled = true;
        hudControl.updateLives(livesRemaining);
        hudControl.updateScore(score);
        hudControl.updateStage(stageNumber);

        // Begin the first level
        nextStage();

        // Wait for key press to set game in action
        waitForKey();
    }


    // Increments stageNumber and generates the next stage
    private void nextStage()
    {
        //Increase & update game variables
        score += 1000 * stageNumber;
        hudControl.updateScore(score);

        stageNumber++;
        hudControl.updateStage(stageNumber);

        // Vector to hold scale of new blocks
        Vector2 scale;
        // Call level gen script to get positions & scale for new blocks
        Vector2[] positions = levelGen.newLevel(stageNumber, out scale);
        GameObject newBlock;

        // Instantiate gameObjects for the blocks
        // TODO: Can this be moved to the levelGen class? I'm having trouble figuring out how to instantiate gameObjects from a class that isn't a monobehaviour
        for (int i = 0; i < positions.Length; i++)
        {
            newBlock = Instantiate(blockTemplate, positions[i], Quaternion.identity);
            newBlock.transform.localScale = scale;
        }

        // Wait for key press to set game in action
        waitForKey();

    }


    // Event listener for when a block is destroyed
    public void blockDestroyed(Vector2 position)
    {
        // Event gets called before block object is destroyed, so be sure to subtract 1 from total here
        int remainingBlocks = GameObject.FindGameObjectsWithTag("block").Length - 1;
        score += 100;
        hudControl.updateScore(score);

        if (remainingBlocks <= 0)
            nextStage();
    }


    // Event listener for when a ball is lost
    public void lostBall()
    {
        // Check if there are any balls remaining
        int remainingBalls = GameObject.FindGameObjectsWithTag("ball").Length - 1;

        if (remainingBalls <= 0)
        {
            livesRemaining--;
            if (livesRemaining <= 0)
            {
                gameOver();
                return;
            }

            hudControl.updateLives(livesRemaining);
            deactivateAllPowerups();
            Instantiate(ballTemplate, Vector3.zero, Quaternion.identity);
            waitForKey();
        }
    }


    // Removes any events the GameManager is subscribed to listen to
    // This needs to happen whenever the main game scene is unloaded.
    // When the scene is reloaded, the scene and game objects will have been destroyed but the event listeners will still exist,
    // leading to this script trying to call a function on an object that no longer exists.
    public void removeEventListeners()
    {
        Block.OnBlockDestroyed -= blockDestroyed;
        Ball.OnLostBall -= lostBall;
    }


    // Adds a value to the lives counter
    public void addLives(int lives)
    {
        livesRemaining += lives;
        hudControl.updateLives(livesRemaining);
    }


    // Adds a value to the score counter
    public void addScore(int value)
    {
        score += value;
        hudControl.updateScore(score);
    }


    // Adds a collected powerup to the activePowerups list
    public void addPowerup(Powerup powerup)
    {
        activePowerups.Add(powerup);
    }


    // Iterates activePowerups list and calls "deactivate" on each
    private void deactivateAllPowerups()
    {
        for (int i = 0; i < activePowerups.Count; i++)
        {
            activePowerups[i].deactivate();
        }

        activePowerups = new List<Powerup>();
    }


    // Event listener for when any keyboard key is pressed
    public void OnAnyKey()
    { 
        if (waitingForKey)
        {
            waitingForKey = false;
            Time.timeScale = 1;
        }
    }


    // Sets timeScale to 0, and waitingForKey to true
    private void waitForKey()
    {
        Time.timeScale = 0;
        waitingForKey = true;
    }


    // Called when lives <= 0
    private void gameOver()
    {
        HUD.enabled = false;
        removeEventListeners();
        gameOverCtrl.gameOver(score,stageNumber);
    }
}
