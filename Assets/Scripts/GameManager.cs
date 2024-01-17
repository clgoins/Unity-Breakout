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
    
    [SerializeField] private LevelGenerator levelGen;

    private List<Powerup> activePowerups;

    void Start()
    {
        livesRemaining = 3;
        score = 0;
        Block.OnBlockDestroyed += blockDestroyed;
        Ball.OnLostBall += lostBall;

        activePowerups = new List<Powerup>();

        nextStage();
    }


    private void nextStage()
    {
        stageNumber++;
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
            deactivateAllPowerups();
            Instantiate(ballTemplate, Vector3.zero, Quaternion.identity);
        }
    }

    public void addLives(int lives)
    {
        livesRemaining += lives;
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
}
