using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Powerup : MonoBehaviour
{
    [SerializeField] protected float fallSpeed;
    [SerializeField] protected Texture2D image;
    [SerializeField] protected Color color;
    [SerializeField] public int probability;
    protected GameManager gameManager;

    private void Start()
    {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        Material mat = GetComponent<SpriteRenderer>().material;
        mat.SetTexture("_Image", image);
        mat.SetColor("_Background_Color", color);

        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().gravityScale = fallSpeed;

    }

    public abstract void activate();

    public abstract void deactivate();

    private void OnTriggerEnter2D(Collider2D other)
    {

        // On collision with paddle, activate powerup and destroy object
        if (other.tag == "paddle")
        {
            activate();
            Destroy(gameObject);
        }


        // On collision with the bottom of the screen, 
        if (other.tag == "theBadWall")
        {
            Destroy(gameObject);
        }
    }


}
