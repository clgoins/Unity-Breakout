using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : Powerup
{


    public override void activate()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.addLives(1);
    }

    public override void deactivate()
    {
        return;
    }
}
