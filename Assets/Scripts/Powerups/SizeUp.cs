using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeUp : Powerup
{
    [SerializeField] private float size;
    [SerializeField] private float maxSize;

    public override void activate()
    {
        Transform paddle = GameObject.FindGameObjectWithTag("paddle").transform;
        Vector3 scale = paddle.localScale;

        if (scale.x + size <= maxSize)
        {
            scale.x += size;
            paddle.localScale = scale;
            gameManager.addPowerup(this);
        }
    }

    public override void deactivate()
    {
        Transform paddle = GameObject.FindGameObjectWithTag("paddle").transform;
        Vector3 scale = paddle.localScale;
        scale.x -= size;
        paddle.localScale = scale;
    }
}
