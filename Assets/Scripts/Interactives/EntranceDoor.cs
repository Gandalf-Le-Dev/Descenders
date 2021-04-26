using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceDoor : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private bool hasPlayerSpawned = false;
    [SerializeField] private Transform spawnPos;

    private void Update()
    {
        if (LevelGeneration.readyForPlayer && hasPlayerSpawned == false)
        {
            // Spawn player object
            Instantiate(player, spawnPos.position, Quaternion.identity, transform.root.parent);
            hasPlayerSpawned = true;
        }
    }
}
