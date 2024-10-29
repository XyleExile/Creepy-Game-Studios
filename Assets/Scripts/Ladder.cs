using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    private bool isOnLadder = false;
    public float climbSpeed = 15f;
    private Rigidbody rb;
    private PlayerController playerController;
    private PlayerMove playerMove;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        playerMove = GetComponent<PlayerMove>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Ladder"))
        {
            isOnLadder = true;

            // Disable normal movement scripts
            if (playerController != null)
                playerController.enabled = false;
            if (playerMove != null)
                playerMove.enabled = false;

            // Zero out vertical velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // Disable gravity
            rb.useGravity = false;

        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Ladder"))
        {
            isOnLadder = false;

            // Enable normal movement scripts
            if (playerController != null)
                playerController.enabled = true;
            if (playerMove != null)
                playerMove.enabled = true;

            // Re-enable gravity
            rb.useGravity = true;

        }
    }

    public void resetLadder()
    {
        isOnLadder = false;
        // Enable normal movement scripts
        if (playerController != null)
            playerController.enabled = true;
        if (playerMove != null)
            playerMove.enabled = true;

        // Re-enable gravity
        rb.useGravity = true;
    }

    void Update()
    {
        if (isOnLadder)
        {
            float verticalInput = Input.GetAxis("Vertical");

            // Move the player up or down the ladder
            Vector3 climbVelocity = new Vector3(0f, verticalInput * climbSpeed, 0f);
            rb.velocity = climbVelocity;

            // prevent horizontal movement while climbing
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);

        }
    }
}
