using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSwitchController : MonoBehaviour
{
    public GameObject generatorDoor;  // Reference to the door that will open
    public GameObject objectToDelete; // Reference to the object you want to delete
    public float doorOpenSpeed = 2f;  // Speed at which the door will open
    public float openDistance = 10f;  // How far the door opens
    public float openSpeed = 5f;      // Speed at which the door opens
    private bool canActivate = false;
    public GameManagerScript gameManager;
    private bool doorClosed = false;
    

    void Update()
    {
        // If the player is within the collision area and presses 'E', move the door
        if (canActivate && Input.GetKeyDown(KeyCode.E) && !doorClosed)
        {
            doorClosed = true;
            StartCoroutine(OpenDoor());
            DestroyObject();
        }
    }

    IEnumerator OpenDoor()
    {
        Vector3 targetPosition = generatorDoor.transform.position + new Vector3(0, 0, openDistance);
        while (generatorDoor.transform.position != targetPosition)
        {
            AudioSource source = GetComponent<AudioSource>();
            
            if (!source.isPlaying)
            {
                source.Play();
            }
            generatorDoor.transform.position = Vector3.MoveTowards(generatorDoor.transform.position, targetPosition, openSpeed * Time.deltaTime);
            yield return null;  // Wait for the next frame
        }
        gameManager.winGame();
    }

    private void DestroyObject()
    {
        if (objectToDelete != null)
        {
            Destroy(objectToDelete);  // Deletes the specified object
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canActivate = true; // Player can activate the door when they collide with the generator
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canActivate = false; // Player can no longer activate the door when they exit the collision
        }
    }
}
