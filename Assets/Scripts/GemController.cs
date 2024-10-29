using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{
    private bool isPlayerNearby = false; // Flag to check if the player is close enough to collect the gem
    public GameObject shark;

    void Update()
    {
        // Check if the player is nearby and the player presses 'E'
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Gem collected!");
            gameObject.SetActive(false);
            if (shark != null)
            {
                shark.GetComponent<SharkController>().StartChase(); // Start the shark chase
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Check if the object leaving the collision is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    public void resetGem()
    {
        gameObject.SetActive(true);
        isPlayerNearby = false;
    }
}
