using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int levelID;

    // Use this method to load the new level and keep the score updated.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Assuming the current score is held in a variable elsewhere in your code, such as a GameManager.
            // Retrieve the previous score saved in PlayerPrefs.
            int previousScore = PlayerPrefs.GetInt("Score", 0);

            // Retrieve the current score that is accumulated during this level.
            int currentScore = ScoreManager.Instance.GetCurrentScore(); // Assuming a GameManager/ScoreManager tracks current score.

            // Calculate the new total score.
            int newTotalScore = previousScore + currentScore;

            // Save the new total score before changing scenes.
            PlayerPrefs.SetInt("Score", newTotalScore);
            PlayerPrefs.Save();

            // Load the specified level.
            SceneManager.LoadScene(levelID);
        }
    }
}

