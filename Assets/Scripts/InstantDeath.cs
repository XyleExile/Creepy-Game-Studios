using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    public PlayerHealth playerHealth; // Reference to the PlayerHealth script
    public float damage = 9999999; // Damage to apply

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Apply damage
            playerHealth.health -= damage;
        }
    }
}
