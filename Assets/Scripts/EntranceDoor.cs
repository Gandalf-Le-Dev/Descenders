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
        if (player != null)
        {
            if (LevelGeneration.readyForPlayer && hasPlayerSpawned == false)
            {
                // Spawn in player object
                Instantiate(player, spawnPos);
                hasPlayerSpawned = true;
            }
        }
    }
}
