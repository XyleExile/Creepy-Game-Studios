using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassController : MonoBehaviour
{
    public GameObject glassPanel;  // Reference to the glass panel game object
    private bool canActivate = false;
    public PlayerHealth playerHealth;
    void Update()
    {
        // If the player is within the collision area and presses 'E', destroy the glass panel
        if (canActivate && Input.GetKeyDown(KeyCode.E))
        {
            playerHealth.TriggerSecondCheckpoint();
            DestroyGlassPanel();
            AudioSource source = GetComponent<AudioSource>();
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
    }

    void DestroyGlassPanel()
    {
        if (glassPanel != null)
        {
            glassPanel.SetActive(false);  // Removes the glass panel from the scene
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canActivate = true; // Player can activate the switch when they collide with the glass switch
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canActivate = false; // Player can no longer activate the switch when they exit the collision
        }
    }

    public void resetGlass()
    {
        glassPanel.SetActive(true);
    }
}
