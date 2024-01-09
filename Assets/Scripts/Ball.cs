using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector2 velocity;
    private Rigidbody2D rb;
    [SerializeField] private float ballSpeed;           // initial speed of the ball
    [SerializeField] private float spread;              // how wide of an angle the balls new velocity can be on contact with the paddle or a block
    [SerializeField] private float ballSpeedIncrement;  // how much the speed increases when breaking a block

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector2.one.normalized;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Prevent the ball from getting stuck going perfectly side to side
        if (velocity.y < 0.25f && velocity.y > -0.25f)
        {
            velocity.y = 1;
            velocity.Normalize();
        }

        rb.velocity = velocity * ballSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "paddle" || collision.collider.tag == "block")
        {
            Transform paddle = collision.collider.transform;

            // Get the point of collision in world space
            ContactPoint2D contact = collision.GetContact(0);

            // Convert the world space contact point to a point on the paddles surface from -1 to 1
            float paddlePoint = (contact.point.x - paddle.position.x) / (paddle.localScale.x / 2);
            
            // remaps from -1 to 1  to  0 to 1
            paddlePoint = (paddlePoint + 1) / 2;

            // new direction will shoot off left if it hits the left side of the paddle or right if hitting the right side
            Vector2 newVelocity = Vector2.Lerp(new Vector2(-spread,-velocity.y), new Vector2(spread,-velocity.y), paddlePoint);

            // normalize the new velocity so speed remains constant
            velocity = newVelocity.normalized;


            // increase the speed of the ball on contact with a block
            if (collision.collider.tag == "block")
                ballSpeed += ballSpeedIncrement;
        }

        if (collision.collider.tag == "topWall")
        {
            velocity.y = -velocity.y;
        }

        if (collision.collider.tag == "sideWall")
        {
            velocity.x = -velocity.x;
        }

    }


}
