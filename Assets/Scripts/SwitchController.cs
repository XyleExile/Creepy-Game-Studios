using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public static int switchesActivated = 0; // Static counter to track activated switches
    private bool canActivate = false;
    private bool isActivated = false; // Flag to check if the switch is already activated

    void Update()
    {
        // Check if the switch can be activated and the E key is pressed
        if (canActivate && !isActivated && Input.GetKeyDown(KeyCode.E))
        {
            isActivated = true; // Set the switch to activated
            switchesActivated++; // Increment the counter of activated switches
            AudioSource source = GetComponent<AudioSource>();
            source.Play();
            Debug.Log("Switch activated by player. Total active: " + switchesActivated);
        }
    }

    // Using OnCollisionEnter to detect collision entry
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            canActivate = true; // The player can activate the switch when they collide with it
        }
    }

    // Using OnCollisionExit to detect collision exit
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            canActivate = false; // The player cannot activate the switch once they stop colliding with it
        }
    }
}
