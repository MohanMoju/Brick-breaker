using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetBall();
    }

    public void ResetBall()
    {
        rb.velocity = Vector2.zero;
        transform.position = new Vector3(0,0,1);

        CancelInvoke();
        Invoke(nameof(SetRandomTrajectory), 1f);
    }

    private void SetRandomTrajectory()
    {
        // Ensure the x component of the initial trajectory is not too low
        float x = Random.Range(-0.75f, 0.75f);
        Vector2 force = new Vector2(x, -1f).normalized;
        rb.AddForce(force * speed, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        // Only adjust the speed if necessary to avoid redundant calculations
        if (Mathf.Abs(rb.velocity.magnitude - speed) > 0.01f)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }
}
