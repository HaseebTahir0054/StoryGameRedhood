using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int score;
    private int lives;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetGame()
    {
        score = 0;
        lives = 3;
        RespawnAllApples();
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
}
