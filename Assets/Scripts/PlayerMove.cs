using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 3;

    private Animator anim;
    private Vector3 move;
    private PlayerController playerController; // Reference to another script that controls the player

    void Start()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>(); 
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = new Vector3(x, 0, z);
        transform.LookAt(transform.position + move);
        transform.position += move * speed * Time.deltaTime;
        UpdateAnim();
    }

    void UpdateAnim()
    {
        anim.SetFloat("Speed", move.magnitude);
    }
}