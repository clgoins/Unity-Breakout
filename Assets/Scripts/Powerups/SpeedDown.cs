using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDown : Powerup
{
    [SerializeField] private float speed;
    [SerializeField] private float minSpeed;

    public override void activate()
    {
        PaddleControl paddle = GameObject.Find("Paddle").GetComponent<PaddleControl>();

        if (paddle.moveSpeed - speed >= minSpeed)
        {
            paddle.moveSpeed -= speed;
            gameManager.addPowerup(this);
        }

    }

    public override void deactivate()
    {
        PaddleControl paddle = GameObject.Find("Paddle").GetComponent<PaddleControl>();
        paddle.moveSpeed += speed;
    }
}
