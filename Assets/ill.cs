using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class KillPLayer : MonoBehaviour
{
   
    public GameObject deathPanel; // Reference to the Death Panel UI

    private void Start()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(false); // Ensure Death Panel is hidden at the start of the game
        }
    }

    public void ShowDeathPanel()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(true); // Activate the Death Panel
        
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false); // Disable instead of destroy
            ShowDeathPanel();
        }
    }
}