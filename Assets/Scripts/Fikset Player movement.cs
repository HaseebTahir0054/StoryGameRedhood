using UnityEngine;
using UnityEngine.UI; // Required for UI elements like Text
using TMPro;

public class FiksetPlayermovement : MonoBehaviour
{
    [SerializeField] private float speed; // Make speed editable in Unity
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    public int extraJumpValue;
    public int maxExtraJumps = 1; // How many extra jumps the player gets
    public int applered = 1; // Sætter værdi for rødt æble
    public int applegreen = 5; // sætter grønt
    public Vector3 respawnPoint;
    public GameObject FallDetector; // Should be assigned in Unity Editor

    // Point system
    private int score; // This will hold the player's score
    public TMP_Text scoreText; // UI Text element to display the score (optional)

    private void Awake()
    {
        // Refer to the Rigidbody and Animator components of the player
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        extraJumpValue = maxExtraJumps;
        respawnPoint = transform.position;

        // Initialize score
        score = 0;
        UpdateScoreText();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flip character
        if (horizontalInput > 0.0f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < 0.0f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Jump logic
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (grounded)  // Single jump
            {
                Jump();
            }
            else if (extraJumpValue > 0)  // Double jump
            {
                Jump();
                extraJumpValue--;  // Reduce available jumps
            }
        }

        // Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);

        if (FallDetector != null)
        {
            Vector2 vector2 = new Vector2(transform.position.x, FallDetector.transform.position.y);
            FallDetector.transform.position = vector2;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true; // Reset grounded when player touches the ground
            extraJumpValue = maxExtraJumps;  // Reset extra jumps when grounded
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false; // Set grounded to false when player leaves the ground
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;  // Set grounded to false when the player jumps
    }

    // Respawn system til start hver gang man falder
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
            
        }
        // Collect coin
        if (collision.gameObject.CompareTag("Redapple"))
        {
            score += applered; // Increment the score
            UpdateScoreText(); // Update the score UI
            Destroy(collision.gameObject); // Destroy the coin object
        }

        if (collision.gameObject.CompareTag("Greenapple"))
        {
            score += 5; // Increment the score
            UpdateScoreText(); // Update the score UI
            Destroy(collision.gameObject); // Destroy the coin object
        }
    }

    // Updates the score display in the UI
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 0, 0), "Score: " + score.ToString());
    }
}
