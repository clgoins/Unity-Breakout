using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMultiplier : Powerup
{
    private static int maxBalls = 50;
    [SerializeField] int multiplier;

    public override void activate()
    {
        GameObject[] activeBalls = GameObject.FindGameObjectsWithTag("ball");
        int ballCount = activeBalls.Length;     // # of current balls in play
        int newBallCount = 0;                   // # of new balls added

        // For each ball currently in the scene
        for (int i = 0; i < ballCount; i++)
        {
            // Create 'multiplier - 1' new balls
            for (int j = 0; j < multiplier - 1; j++)
            {
                // But only if it wouldn't push the # of currently active balls > maxBalls
                if (ballCount + newBallCount < maxBalls)
                {
                    // Use the gameManagers ballTemplate object to instantiate the new ball
                    GameObject newBall = Instantiate(gameManager.ballTemplate, activeBalls[i].transform.position, Quaternion.identity);
                    
                    // Give the new ball a random direction
                    Vector2 newDirection = Vector2.zero;
                    newDirection.x = Random.Range(-1.0f,1.0f);
                    newDirection.y = Random.Range(-1.0f,1.0f);
                                        
                    // Normalize the new direction and pass it to the new ball object
                    newBall.GetComponent<Ball>().setDirection(newDirection.normalized);

                    newBallCount++;
                }
            }
        }
    }

    public override void deactivate()
    {
        return;
    }
}
