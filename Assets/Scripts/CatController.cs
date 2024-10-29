using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    public Transform tightSpace; // Reference to the tight space
    public GameObject key;
    public int reqAmount;
    public float moveSpeed = 3f;

    private bool isMoving = false; // To track if the cat is currently moving
    private Transform playerTransform; // Reference to the player's transform
    private Transform keyTransform; // Reference to the key
    private PlayerController playerController; // Reference to the PlayerController script
    private Vector3 originalPosition; // To store the cat's starting position
    private bool isPlayerNearby = false;

    void Start()
    {
        // Find the player in the scene 
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = playerTransform.GetComponent<PlayerController>();
        keyTransform = key.transform;

        // Store the cat's original position
        originalPosition = transform.position;
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && playerController.getFoodCollected() >= reqAmount && !isMoving)
        {
            isMoving = true;
            StartCoroutine(RetrieveKey());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    private IEnumerator RetrieveKey()
    {

        if (tightSpace.name == "TightSpace") {
            // Move to the tight space
            yield return MoveToPosition(tightSpace.position, transform);

            // Move back to the original position
            if (keyTransform != null) {
                yield return MoveToPosition(originalPosition + new Vector3(-1.0f, 1.0f, 0), keyTransform);
            }
            yield return MoveToPosition(originalPosition, transform);
            
        } else {
            // Step 1: Move the cat to the tight space
            yield return MoveToPosition(tightSpace.position, transform);

            // Step 2: Move the cat to the key's position
            Vector3 keyPosition = keyTransform.position;
            yield return MoveToPosition(keyPosition, transform);

            // Step 3: Move the key to the tight space
            yield return MoveToPosition(tightSpace.position, keyTransform);

            // Step 4: Move the cat back to the tight space (ensure it's clear the cat returns to the space, not just the key)
            yield return MoveToPosition(tightSpace.position, transform);

            // Step 5: Move the key to the original position
            Vector3 originalKeyPosition = originalPosition + new Vector3(-1.0f, 1.0f, 0); // Assuming you want the key slightly offset from the cat's original position
            yield return MoveToPosition(originalKeyPosition, keyTransform);

            // Step 6: Move the cat to its original position
            yield return MoveToPosition(originalPosition, transform);
        }
    }
    
    private IEnumerator MoveToPosition(Vector3 destination, Transform objectToMove)
    {
        if (objectToMove == null) yield break;  // Exit if the object no longer exists

        Vector3 startPosition = objectToMove.position;  // Start position records the initial position

        while (Vector3.Distance(objectToMove.position, destination) > 0.1f)
        {
            if (objectToMove == null) yield break;  // Check again before updating position

            // Calculate the new position towards the destination
            Vector3 newPosition = Vector3.MoveTowards(objectToMove.position, destination, moveSpeed * Time.deltaTime);
            objectToMove.position = newPosition;  // Update the object's position
            
            yield return null;
        }

        if (objectToMove == null) yield break;  // Final check before setting the final position

        objectToMove.position = destination;  // Ensure the object is exactly at the destination
    }

}