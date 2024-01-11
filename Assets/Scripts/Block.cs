using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public static event System.Action OnBlockDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        //Get a random value between 0 and 5 inclusive and pass it to the block materials color property
        float colorValue = Mathf.Floor(Random.Range(0, 5.99f));
        GetComponentInChildren<SpriteRenderer>().material.SetFloat("_Color", colorValue);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "ball")
        {
            OnBlockDestroyed.Invoke();
            GameObject.Destroy(gameObject);
        }
    }


}
