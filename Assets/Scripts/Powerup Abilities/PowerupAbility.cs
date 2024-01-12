using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PowerupAbilities")]
public class PowerupAbility : ScriptableObject
{
    [SerializeField] public Texture2D image;
    [SerializeField] public Color color;

    // Attributes the powerup can potentially modify
    [SerializeField] private int lifeModifier;
    [SerializeField] private int ballModifier;
    [SerializeField] private float speedModifier;
    [SerializeField] private float sizeModifier;

    public void activate()
    {

        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();


        // Makes a call to the game manager to add lives
        if (lifeModifier != 0)
        {
            gm.addLives(lifeModifier);
        }

        // For each ball in the scene, add a number of new balls in the same position, and send the off in random directions
        if (ballModifier != 0)
        {
            GameObject[] activeBalls = GameObject.FindGameObjectsWithTag("ball");

            foreach (GameObject ball in activeBalls)
            {
                
                for (int i = 0; i < ballModifier - 1; i++)
                {
                    Debug.Log("Adding ball");
                    GameObject newBall = Instantiate(gm.ballTemplate, ball.transform.position, Quaternion.identity);
                    newBall.GetComponent<Ball>().setDirection(Random.insideUnitCircle);
                }
            }
        }

        // Change the max movement speed of the paddle
        if (speedModifier != 0)
        {
            PaddleControl paddle = GameObject.FindGameObjectWithTag("paddle").GetComponent<PaddleControl>();
            paddle.moveSpeed += speedModifier;
        }

        // Change the size of the paddle
        if (sizeModifier != 0)
        {
            GameObject paddle = GameObject.FindGameObjectWithTag("paddle");
            Vector3 newScale = paddle.transform.localScale;
            newScale.x += sizeModifier;
            paddle.transform.localScale = newScale;
        }
    }
}
