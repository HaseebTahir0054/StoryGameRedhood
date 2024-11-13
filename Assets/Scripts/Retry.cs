using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    // Method to handle Retry button action
    public void RetryGame()
    {
        // Reset score, lives, and respawn all apples
        GameManager.Instance.ResetGame();

        // Reload the first scene (assuming it is at index 0)
        SceneManager.LoadScene(0);
    }
}
