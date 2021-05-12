using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class EntranceDoor : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private bool hasPlayerSpawned = false;
    [SerializeField] private Transform spawnPos;
    private PlayerMovement playerRef;

    private void Start()
    {
        playerRef = FindObjectOfType<PlayerMovement>();
        if (playerRef == null) return;
        hasPlayerSpawned = true;
        playerRef.transform.position = spawnPos.position;
    }

    private void Update()
    {
        /*
         * Spawn player if the first level
         */
        if (LevelGeneration.readyForPlayer && hasPlayerSpawned == false)
        {
            // Spawn player object
            Instantiate(player, spawnPos.position, Quaternion.identity, transform.root.parent);
            hasPlayerSpawned = true;
        }
    }
}
