using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    public Transform playerTransform; // Assign this in the inspector or find it dynamically
    public float chaseSpeed = 5f;
    private bool isChasing = false;

    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the shark at the start
        initialPosition = transform.position;
    }

    void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
    }

    public void StartChase()
    {
        isChasing = true;
        Debug.Log("Shark is now chasing the player!");
        AudioSource source = GetComponent<AudioSource>();
        if (!source.isPlaying)
        {
            source.Play();
        }
    }

    private void ChasePlayer()
    {
        // Simple chasing logic - move towards the player at each frame
        Vector3 position = Vector3.MoveTowards(transform.position, playerTransform.position, chaseSpeed * Time.deltaTime);
        transform.position = position;
    }

    public void resetPosition()
    {
        transform.position = initialPosition;
        isChasing = false;

    }
}
