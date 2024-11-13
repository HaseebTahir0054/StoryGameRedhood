using UnityEngine;

public class QuitButton : MonoBehaviour
{
    // Method to handle Quit button action
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}