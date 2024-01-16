using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : Powerup
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;


    public override void activate()
    {
        PaddleControl paddle = GameObject.Find("Paddle").GetComponent<PaddleControl>();

        if (paddle.moveSpeed + speed <= maxSpeed)
        {
            paddle.moveSpeed += speed;
            gameManager.addPowerup(this);
        }
    }

    public override void deactivate()
    {
        PaddleControl paddle = GameObject.Find("Paddle").GetComponent<PaddleControl>();
        paddle.moveSpeed -= speed;
    }
}
