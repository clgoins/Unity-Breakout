using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : Powerup
{
    [SerializeField] private int lifeCount;

    public override void activate()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.addLives(lifeCount);
    }

    public override void deactivate()
    {
        return;
    }
}
