using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Powerup : MonoBehaviour
{
    [SerializeField] private float fallSpeed;
    private Rigidbody2D rb;
    private Vector3 velocity;
    [SerializeField] private GameManager gameManager;
    [SerializeField] public List<PowerupAbility> abilities;
    int powerPicker;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        velocity = Vector3.zero;
        velocity.y = -fallSpeed;

        powerPicker = (int)Mathf.Floor(Random.Range(0, abilities.Count));
        if (powerPicker >= abilities.Count)
            powerPicker = abilities.Count - 1;

        GetComponent<SpriteRenderer>().material.SetColor("_Background_Color", abilities[powerPicker].color);
        GetComponent<SpriteRenderer>().material.SetTexture("_Image", abilities[powerPicker].image);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = velocity;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag == "paddle")
        {
            abilities[powerPicker].activate();
            Destroy(gameObject);
        }
    }


}
