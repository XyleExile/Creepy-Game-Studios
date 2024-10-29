using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiraffeController : MonoBehaviour
{
    public float detectionRadius = 2;
    public GameObject[] floorTiles;

    private Transform playerTransform;
    private bool tilesDisappeared = false;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (tilesDisappeared) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);


        if (distanceToPlayer < detectionRadius)
        {
            PlayerController playerController = playerTransform.GetComponent<PlayerController>();
            if (playerController != null && Input.GetKeyDown(KeyCode.E))
            {
                MakeFloorTilesDisappear();
            }
        }
    }

    private void MakeFloorTilesDisappear()
    {
        foreach (GameObject tile in floorTiles)
        {
            if (tile != null)
            {
                Destroy(tile);
            }
        }
        tilesDisappeared = true;
    }
}
