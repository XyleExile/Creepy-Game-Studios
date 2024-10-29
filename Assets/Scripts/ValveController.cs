using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveController : MonoBehaviour
{
    private bool canActivate = false;
    private bool isActivated = false;
    public GameObject[] toxic;

    void Update()
    {
        if (canActivate && !isActivated && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Switch activated");
            isActivated = true; // Set the switch to activated
            MakeToxicDisappear();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is the player
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player can activate the switch");
            canActivate = true; // The player can activate the switch when they collide with it
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Check if the object leaving the collision is the player
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player cannot activate the switch anymore");
            canActivate = false; // The player cannot activate the switch once they stop colliding with it
        }
    }

    private void MakeToxicDisappear()
    {
        Debug.LogWarning("Toxic is disappearing");
        foreach (GameObject item in toxic)
        {
            if (item != null)
            {
                Debug.Log("Destroying toxic object");
                Destroy(item);
                AudioSource source = GetComponent<AudioSource>();
                source.Play();
            }
        }
    }
}
