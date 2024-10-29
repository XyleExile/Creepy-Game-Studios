using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public float penalty;
    public Image healthBar;
    public float healthDecreaseRate = 0.1f;
    private bool isDead = false;
    public GameManagerScript gameManager;

    public float previousHealth;

    [SerializeField] ParticleSystem collectParticle = null;
    public GameObject checkpoint1; // First checkpoint
    public GameObject checkpoint2; // Second checkpoint
    public GameObject currentCheckpoint; // The checkpoint the player will respawn at
    public Vector3 checkpointPosition;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        SetCheckpoint(checkpoint1);
    }

    // Update is called once per frame
    void Update()
    {
        if (health >= 0 && !isDead)
        {
            previousHealth = health;
            healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
            health -= healthDecreaseRate * Time.deltaTime;
        }

        if (health <= 0 && !isDead) // Check for death condition
        {
            isDead = true;
            Collect(); // Play particle effect
            
            // Check if this is temporary death
            if (previousHealth > maxHealth * 0.1f)
            {
                gameManager.tempDeathScreen();
            }
            else
            {
                gameManager.gameOver();
            }

            // Destroy the player object after determining death state
            gameObject.SetActive(false);
        }
    }

    public void Collect()
    {
        // Set the position of the particle system to the player's position
        collectParticle.transform.position = transform.position; // Move to the player's position

        // Optional: if you want the particles to be detached, do this:
        collectParticle.transform.SetParent(null); // Detach particle system from player
        
        collectParticle.Play(); // Play the particle effect
    }

    public void IncreaseHealth(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void respawn() // Original function name retained
    {
        isDead = false;
        // Reset player health to previousHealth - maxHealth * 0.1f
        health = Mathf.Max(previousHealth - penalty, 0); // Prevent negative health
        
        // Move player to checkpoint position
        transform.position = checkpointPosition; // Use the stored position
        gameObject.SetActive(true);
    }

    public void SetCheckpoint(GameObject checkpoint)
    {
        currentCheckpoint = checkpoint; 
        checkpointPosition = currentCheckpoint.transform.position;
    }

    public void TriggerSecondCheckpoint()
    {
        SetCheckpoint(checkpoint2); // Set the current checkpoint to checkpoint2
        Debug.Log("Second checkpoint activated.");
    }
}
