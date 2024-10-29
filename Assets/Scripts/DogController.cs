using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    public float detectionRadius = 10; // Radius within which the player is detected
    public Transform doorTransform; // Reference to the door's Transform

    private Transform playerTransform; // Reference to the player's Transform
    private bool isMoving = false;
    private OpenDoor door; // Reference to the OpenDoor component

    public Animator animator;

    private void Start()
    {
        // Assuming you can find the player by tag or any other method
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // Assuming the doorTransform has the OpenDoor component
        if (doorTransform != null)
        {
            door = doorTransform.GetComponent<OpenDoor>();
        }
        else
        {
            Debug.LogWarning("Door Transform is not assigned in the inspector.");
        }

    }

    private void Update()
    {
        if (isMoving) return;

        // Check distance to player
        if (Vector3.Distance(transform.position, playerTransform.position) < detectionRadius)
        {

            // Check if the player is carrying food
            if (playerTransform.GetComponent<PlayerController>().getFoodCollected() > 0 && Input.GetKeyDown(KeyCode.E))
            {
                MoveToDoor();
            }
        }

        // Check distance to door
        if (Vector3.Distance(transform.position, doorTransform.position) < detectionRadius)
        {
            // Trigger door opening if within detection radius
            if (door != null)
            {
                door.OnDogApproach();
            }
        }
    }

    private void MoveToDoor()
    {
        // Move the dog to a point on the door at the same height as the dog
        if (doorTransform != null)
        {
            isMoving = true;

            animator.SetBool("isRunning", true);
            // Adjust the target position to match the dog's y-coordinate
            Vector3 targetPosition = new Vector3(doorTransform.position.x, transform.position.y, doorTransform.position.z);

            // Start moving towards the adjusted position
            StartCoroutine(MoveTowards(targetPosition));
        }
    }


    private IEnumerator MoveTowards(Vector3 targetPosition)
    {
        float timeElapsed = 0f;
        float duration = 2f; // Duration of the movement

        Vector3 startPosition = transform.position;

        while (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Ensure the final position is accurate
        // Stop running animation after reaching the target
        animator.SetBool("isRunning", false);
        isMoving = false;
    }


}


