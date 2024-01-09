using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleControl : MonoBehaviour
{
    private Vector2 velocity;
    private float desiredVelocity;
    [SerializeField] private float acceleration;
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;
    public Vector2 debugRBVelocity;    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // add the desiredVelocity to the actual velocity, factoring in acceleration over time
        velocity.x += desiredVelocity * acceleration * Time.deltaTime;

        // Actual velocity should be from range 1 to -1
        velocity.x = Mathf.Clamp(velocity.x, -1, 1);


        // If desired velocity is 0, ramp down the actual velocity, factoring in acceleration over time
        if (desiredVelocity == 0)
        {
            if (velocity.x < -0.1f)
            {
                velocity.x += acceleration * Time.deltaTime;
            }
            else if (velocity.x > 0.1f)
            {
                velocity.x -= acceleration * Time.deltaTime;
            }
            // Once the velocity is sufficiently close to 0, set it directly to 0 to prevent jittering
            else
            {
                velocity.x = 0;
            }
        }

        // make sure this stays at 0 to prevent up/down movement due to physics engine
        velocity.y = 0;
        
        // Apply the velocity to the rigidbody to move the gameobject
        rb.velocity = velocity * moveSpeed;
    }


    private void OnMove(InputValue inputValue)
    {
        desiredVelocity = inputValue.Get<float>();

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "ball")
        {

        }
    }

}
