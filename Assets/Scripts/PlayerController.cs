using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float jumpCooldown = 10f;
    private float lastJumpTime;
    private Rigidbody rb;
    private Animator anim;
    private PlayerHealth playerHealth;

    // Counter to keep track of food and keys collected
    private int foodCollected = 0;
    private int keyCollected = 0;

    private List<GameObject> nearbyFood = new List<GameObject>();
    private List<GameObject> nearbyKeys = new List<GameObject>();
    private List<GameObject> nearbyHealthPacks = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 1f;
        Physics.gravity = new Vector3(0, -20f, 0);
        anim = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from horizontal (A/D, Left/Right) and vertical (W/S, Up/Down) axis
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a new vector for movement based on input
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Normalize the movement vector to maintain consistent speed in all directions
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        // Move the player
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Check if space is pressed and the cooldown has elapsed
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastJumpTime >= jumpCooldown)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("Jump");
            lastJumpTime = Time.time; // Update the last jump time
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (nearbyFood.Count > 0)
            {
                foodCollected++;  // Increase the food collected counter
                GameObject foodToDestroy = nearbyFood[0];  // Get the first food object
                nearbyFood.RemoveAt(0);  // Remove it from the list
                Debug.Log("Food collected: " + foodCollected);  // Print the updated food count
                Destroy(foodToDestroy);  // Destroy the food object
                AudioSource source = GetComponent<AudioSource>();
                if (!source.isPlaying)
                {
                    source.Play();
                }
            }
            else if (nearbyKeys.Count > 0)
            {
                keyCollected++;  // Increase the key collected counter
                GameObject keyToDestroy = nearbyKeys[0];  // Get the first key object
                nearbyKeys.RemoveAt(0);  // Remove it from the list
                Debug.Log("Key collected: " + keyCollected);  // Print the updated key count
                Destroy(keyToDestroy);  // Destroy the key object
                AudioSource source = GetComponent<AudioSource>();
                if (!source.isPlaying)
                {
                    source.Play();
                }
            }
            else if (nearbyHealthPacks.Count > 0)
            {
                // Increase player's health
                GameObject healthPack = nearbyHealthPacks[0];
                nearbyHealthPacks.RemoveAt(0);
                // Each health pack restores 40 health points
                playerHealth.IncreaseHealth(40f);
                Debug.Log("Health pack collected. Current Health: " + playerHealth.health);
                Destroy(healthPack);
            }
        }

    }
    void FixedUpdate()
    {
        if (!IsGrounded())
        {
            rb.AddForce(Vector3.down * 7f);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            nearbyFood.Add(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Key"))
        {
            nearbyKeys.Add(other.gameObject);
        }
        else if (other.gameObject.CompareTag("HealthPack"))
        {
            nearbyHealthPacks.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Food") && nearbyFood.Contains(other.gameObject))
        {
            nearbyFood.Remove(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Key") && nearbyKeys.Contains(other.gameObject))
        {
            nearbyKeys.Remove(other.gameObject);
        }
        else if (other.gameObject.CompareTag("HealthPack"))
        {
            nearbyHealthPacks.Remove(other.gameObject);
        }
    }


    public int getFoodCollected()
    {
        return foodCollected;
    }

    public int getKeyCollected()
    {
        return keyCollected;
    }

    public float getLastJumpTime()
    {
        return lastJumpTime;
    }

    public float getJumpCooldown()
    {
        return jumpCooldown;
    }
}