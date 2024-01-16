using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMultiplier : Powerup
{
    private static int maxBalls = 125;
    [SerializeField] int multiplier;

    public override void activate()
    {
        GameObject[] activeBalls = GameObject.FindGameObjectsWithTag("ball");

        // For each ball currently in the scene
        for (int i = 0; i < activeBalls.Length; i++)
        {
            // Create multiplier new balls
            for (int j = 0; j < multiplier; j++)
            {
                // But only if it wouldn't push the # of currently active balls > maxBalls
                if (activeBalls.Length < maxBalls)
                {
                    // Use the gameManagers ballTemplate object to instantiate the new ball
                    GameObject newBall = Instantiate(gameManager.ballTemplate, activeBalls[i].transform.position, Quaternion.identity);
                    
                    // Give the new ball a random direction
                    Vector2 newDirection = Vector2.zero;
                    newDirection.x = Random.Range(-10.0f,10.0f);
                    newDirection.y = Random.Range(-10.0f, 10.0f);

                    // Normalize the new direction and pass it to the new ball object
                    newBall.GetComponent<Ball>().setDirection(newDirection.normalized);
                }
            }
        }
    }

    public override void deactivate()
    {
        return;
    }
}
