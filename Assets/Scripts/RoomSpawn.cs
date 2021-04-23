using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] rooms;
    private bool flag = false;

    private void Update()
    {
        if (LevelGeneration.firstStageDone && flag == false)
        {
            flag = true;
            // Generate room
            int rand = Random.Range(0, rooms.Length);
            Instantiate(rooms[rand], transform);
        }
    }
}
