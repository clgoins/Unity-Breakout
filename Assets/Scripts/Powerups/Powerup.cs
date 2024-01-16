using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public abstract class Powerup : MonoBehaviour
{
    [SerializeField] protected float fallSpeed = 1.0f;
    private Vector3 velocity;

    [SerializeField] protected Texture2D image;
    [SerializeField] protected Color color;
    [SerializeField] protected string powerupName;
    [SerializeField] public int probability;
    protected GameManager gameManager;


    private void Start()
    {
        velocity = Vector3.zero;
        velocity.y = fallSpeed;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        Material mat = GetComponent<SpriteRenderer>().material;
        mat.SetTexture("_Image", image);
        mat.SetColor("_Background_Color", color);

        GetComponent<BoxCollider2D>().isTrigger = true;

    }

    void Update()
    {
        transform.position -= velocity * Time.deltaTime;
    }

    public abstract void activate();

    public abstract void deactivate();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "paddle")
        {
            activate();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "theBadWall")
        {
            Destroy(gameObject);
        }
    }

}
