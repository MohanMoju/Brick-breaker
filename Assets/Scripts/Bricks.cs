using UnityEngine;

public class Bricks : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public AudioSource audioSource;
    public Sprite[] states;
    public int health; // Use camelCase for consistency
    public bool unbreakable;
    public int points = 100;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ResetBricks();
    }

    public void ResetBricks()
    {
        gameObject.SetActive(true);

        if (!unbreakable)
        {
            health = states.Length;
            if (states.Length > 0) // Check if states array is not empty
            {
                spriteRenderer.sprite = states[health - 1];
            }
        }
    }

    private void Hit()
    {
        if (unbreakable)
        {
            return;
        }

        health--;

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
        else if (health - 1 < states.Length && health - 1 >= 0) // Additional check for valid index
        {
            spriteRenderer.sprite = states[health - 1];
        }

        // Notify Game Manager of the hit
        GameManager.Instance.OnBrickHit(this);

        // Play sound if audioSource is assigned
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            Hit();
        }
    }
}
