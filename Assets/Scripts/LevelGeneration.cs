using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGeneration : MonoBehaviour
{
    /*
     * TODO: Populate the rooms array with rooms folder content on awake
     */

    [SerializeField] private Transform[] startingPositions;

    [SerializeField] private GameObject[] rooms;

    private int direction;
    [SerializeField] private float moveAmount;

    private float timeBtwRoom;
    [SerializeField] private float startTimeBtwRoom = 0.25f;

    void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);
        direction = Random.Range(1, 6);
    }

    private void Update()
    {
        if (timeBtwRoom <= 0)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    private void Move()
    {
        if (direction == 1 || direction == 2) // Move Right
        {
            Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
            transform.position = newPos;
        }
        else if (direction == 3 || direction == 4) // Move Left
        {
            Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
            transform.position = newPos;
        }
        else if (direction == 5) // Move Down chances of going down (1/5) is less than left or right (2/5)
        {
            Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
            transform.position = newPos;
        }

        Instantiate(rooms[0], transform.position, Quaternion.identity);
        direction = Random.Range(1, 6);
    }
}