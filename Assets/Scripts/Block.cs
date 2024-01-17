using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Event to throw when a block is destroyed. Takes the X and Y position of the block.
    public static event System.Action<Vector2> OnBlockDestroyed;
    
    private Vector2 centerPoint;    // Blocks origin is in top right corner; this vector represents the center of the block


    // Start is called before the first frame update
    void Start()
    {
        //Get a random value between 0 and 5 inclusive and pass it to the block materials color property
        float colorValue = Mathf.Floor(Random.Range(0, 5.99f));
        GetComponentInChildren<SpriteRenderer>().material.SetFloat("_Color", colorValue);

        centerPoint.x = transform.position.x + transform.localScale.x / 2;
        centerPoint.y = transform.position.y - transform.localScale.y / 2;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "ball")
        {
            OnBlockDestroyed.Invoke(centerPoint);
            GameObject.Destroy(gameObject);
        }
    }


}
