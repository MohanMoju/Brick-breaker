using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D body;
    public float speed = 30f;
    private Vector2 direction;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetPaddle();
    }

    private void Update()
    {
        // Check for paddle movement inputs
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        // Apply velocity directly for consistent movement
        body.velocity = direction * speed;
    }

    public void ResetPaddle()
    {
        // Reset paddle position and stop movement
        body.velocity = Vector2.zero;
        transform.position = new Vector3(0f, transform.position.y, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            // Additional logic for ball-paddle interaction can be added here if needed
        }
    }
}
