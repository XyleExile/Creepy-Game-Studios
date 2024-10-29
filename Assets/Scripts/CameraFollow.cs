using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform; 
    public Vector3 offset; 
    public float followSpeed = 2f; 

    private void Start()
    {
        if (playerTransform == null)
        {
            // Find the player in the scene if not assigned
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Set the initial position of the camera
        transform.position = playerTransform.position + offset;
    }

    private void LateUpdate()
    {
        // Calculate the desired position with offset
        Vector3 targetPosition = playerTransform.position + offset;

        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}