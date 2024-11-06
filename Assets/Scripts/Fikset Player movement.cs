using UnityEngine;
using UnityEngine.UI; // Required for UI elements like Text and Slider
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class FiksetPlayermovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    public int extraJumpValue;
    public int maxExtraJumps = 1;
    public int applered = 1;
    public int applegreen = 5;
    public Vector3 respawnPoint;
    public GameObject FallDetector;

    // Point system
    private int score;
    public TMP_Text scoreText;

    // Health system
    public int maxLives = 3;
    private int currentLives;
    public Slider healthBar;
    public TMP_Text livesText;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        extraJumpValue = maxExtraJumps;
        respawnPoint = transform.position;

        // Initialize score and lives
        score = 0;
        currentLives = maxLives;
        UpdateScoreText();
        UpdateHealthUI();
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
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                Jump();
            }
            else if (extraJumpValue > 0)
            {
                Jump();
                extraJumpValue--;
            }
        }

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
            grounded = true;
            extraJumpValue = maxExtraJumps;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            LoseLife();
        }

        if (collision.gameObject.CompareTag("Redapple"))
        {
            score += applered;
            UpdateScoreText();
            collision.gameObject.SetActive(false); // Disable instead of destroy
        }

        if (collision.gameObject.CompareTag("Greenapple"))
        {
            score += applegreen;
            UpdateScoreText();
            collision.gameObject.SetActive(false); // Disable instead of destroy
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    private void LoseLife()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            // Reset lives, score, and respawn apples when lives are depleted
            RestartGame();
        }
        else
        {
            // Respawn player at the last checkpoint
            transform.position = respawnPoint;
        }

        UpdateHealthUI();
    }

    private void RestartGame()
    {
        // Reload the currently active scene to restart the game completely
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentLives / maxLives;
        }

        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives;
        }
    }

    private void RespawnAllApples()
    {
        GameObject[] redApples = GameObject.FindGameObjectsWithTag("Redapple");
        GameObject[] greenApples = GameObject.FindGameObjectsWithTag("Greenapple");

        foreach (GameObject apple in redApples)
        {
            apple.SetActive(true);
        }

        foreach (GameObject apple in greenApples)
        {
            apple.SetActive(true);
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 0, 0), "Score: " + score.ToString());
    }

    public void SetRespawnPoint(Vector3 newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
    }

  

   

}
