using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openDistance = 10f; // Distance to slide the door open on the z-axis
    public float openSpeed = 5f;    // Speed at which the door opens
    public float minRadius = 1f;    // Minimum distance the dog must be within to trigger the door
    public int keyReqAmount;
    public int switchReqAmount = 3;

    private Vector3 initialPosition; // Store the initial position of the door
    private bool isOpening = false;  // Flag to check if the door is already opening
    private bool isPlayerNearby = false;
    private GameObject nearbyPlayer; // To reference the player when nearby

    void Start()
    {
        // Store the initial position of the door
        initialPosition = transform.position;
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && nearbyPlayer != null)
        {
            PlayerController playerController = nearbyPlayer.GetComponent<PlayerController>();
            if (playerController != null && playerController.getKeyCollected() >= keyReqAmount)
            {
                isOpening = true;
            }

        }
        else if (SwitchController.switchesActivated >= switchReqAmount)
        {
            isOpening = true;
        }

        // Existing code to open the door
        if (isOpening)
        {
            AudioSource source = GetComponent<AudioSource>();
            
            if (!source.isPlaying)
            {
                source.Play();
            }

            Vector3 targetPosition = initialPosition + new Vector3(0, 0, openDistance);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, openSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isOpening = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Animal") && Vector3.Distance(collision.transform.position, transform.position) <= minRadius)
        {
            isOpening = true; // Directly open for the animal
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = true;
            nearbyPlayer = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = false;
            nearbyPlayer = null;
        }
    }

}

