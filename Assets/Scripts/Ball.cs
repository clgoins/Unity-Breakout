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

    public static event System.Action OnLostBall;


    // Awake is called before the first frame update
    void Awake()
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

        // Collision with paddle should result in ball moving UP and either left or right depending on which side of the paddle it hits.
        if (collision.collider.tag == "paddle")
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
                
        }


        // Collision with top wall should result in ball moving DOWN with same x velocity
        if (collision.collider.tag == "topWall")
        {
            velocity.y = -velocity.y;
        }


        // Collision with side wall should reverse x velocity but maintain same y velocity
        if (collision.collider.tag == "sideWall")
        {
            velocity.x = -velocity.x;
        }


        // Collision with top/bottom of block should reverse y velocity, collision with sides of block should reverse x velocity
        if(collision.collider.tag == "block")
        {
            // speed the ball up a bit for each block destroyed
            ballSpeed += ballSpeedIncrement;

            // get the position and size of the block
            Transform block = collision.collider.transform;
            Vector3 blockSize = block.localScale;

            // if ball is between top & bottom of block, it must be on the side, so reverse x velocity
            if (transform.position.y <= block.position.y && transform.position.y > block.position.y - blockSize.y)
            {
                velocity.x = -velocity.x;
            }

            // otherwise the ball is approaching from the top or bottom, so reverse y velocity
            else
            {
                velocity.y = -velocity.y;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the ball makes it past the paddle to the bottom of the screen, tell the game manager a ball was lost
        if (collision.tag == "theBadWall")
        {
            OnLostBall.Invoke();
            Destroy(gameObject);
        }
    }


    // Used by powerups when instantiating new balls
    public void setDirection(Vector2 newDirection)
    {
        velocity = newDirection.normalized;
    }
}
