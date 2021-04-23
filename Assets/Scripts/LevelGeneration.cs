using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private int levelWidth;
    [SerializeField] private int levelHeight;
    [SerializeField] private int scale;
    [SerializeField] private GameObject[] startingRooms;
    [SerializeField] private GameObject[] pathRooms; //Critical path rooms 0:LR, 1:LRB, 2:LRT, 3:LRTB
    [SerializeField] private GameObject[] fillerRooms;
    [SerializeField] private GameObject[] endingRooms;

    private GameObject[,] roomArray;
    private List<Vector2> loadedRooms = new List<Vector2>();
    [SerializeField] private float roomGenDelayTimer = 0.25f;

    public static bool firstStageDone = false;
    public static bool readyForPlayer = false;

    private int direction; // 0 & 1: Right, 2 & 3: Left, 4: Down
    private float delay = 0f;
    private float completionTime = 0f;

    private int seed;

    private void Start()
    {
        firstStageDone = false;
        readyForPlayer = false;
        roomArray = new GameObject[levelWidth, levelHeight];
        Random.InitState(seed);
        transform.position = new Vector2(Random.Range(0, levelWidth), 0);
        CreateRoom(startingRooms[0]);
        if (transform.position.x == 0)
            direction = 0;
        else if (transform.position.x == levelWidth - 1)
            direction = 2;
        else
            direction = Random.Range(0, 4);
    }

    private void Update()
    {
        if(!firstStageDone) completionTime += Time.deltaTime;
        if (!firstStageDone && delay <= 0)
        {
            delay = roomGenDelayTimer;
            if (direction == 0 || direction == 1)
            { // Right
                if (transform.position.x < levelWidth - 1)
                {
                    transform.position += Vector3.right;
                    int rand = 0;
                    CreateRoom(pathRooms[rand]);
                    direction = Random.Range(0, 5);
                    if (direction == 2)
                        direction = 1;
                    else if (direction == 3)
                        direction = 4;
                }
                else
                    direction = 4;
            }
            else if (direction == 2 || direction == 3)
            { // Left
                if (transform.position.x > 0)
                {
                    transform.position += Vector3.left;
                    int rand = 0;
                    CreateRoom(pathRooms[rand]);
                    direction = Random.Range(0, 5);
                    if (direction == 0)
                        direction = 2;
                    else if (direction == 1)
                        direction = 4;
                }
                else
                    direction = 4;
            }
            else if (direction == 4)
            { // Down
                if (transform.position.y > -levelHeight + 1)
                {
                    Destroy(GetRoom(transform.position));
                    int rand = (Random.Range(0, 2) == 0) ? 1 : 3; // If equals 0 then choose LRB otherwise choose LRTB
                    CreateRoom(pathRooms[rand]);
                    transform.position += Vector3.down;
                    int rand2 = Random.Range(0, 4);
                    if (rand2 == 0)
                    {
                        if (transform.position.y > -levelHeight + 1)
                        {
                            CreateRoom(pathRooms[3]); // Needs top and bottom opening
                            transform.position += Vector3.down;
                        }
                        else
                        {
                            direction = 4;
                            return;
                        }
                    }

                    int rand3 = Random.Range(2, 4);
                    CreateRoom(pathRooms[rand3]);

                    if (transform.position.x == 0)
                        direction = 0;
                    else if (transform.position.x == levelWidth - 1)
                        direction = 2;
                    else
                        direction = Random.Range(0, 4);
                }
                else
                { // If trying to generate another room down and fail means we are at the bottom
                    Destroy(GetRoom(transform.position));
                    CreateRoom(endingRooms[0]);
                    FillMap();
                    firstStageDone = true;
                    Debug.Log("Completion Time in seconds: " + completionTime + " seconds" + "\n" + "Completion Time in milliseconds: " + completionTime * 1000);
                }
            }
        }
        else
        {
            delay -= Time.deltaTime;
        }
    }

    private void CreateRoom(GameObject room)
    {
        GameObject temp = Instantiate(room, transform.position * scale, Quaternion.identity);
        int x = (int)transform.position.x;
        int y = -(int)transform.position.y;
        roomArray[x, y] = temp;
        loadedRooms.Add(new Vector2(x, y));
    }

    private void FillMap()
    {
        // Generate all the rooms outside of the critical path
        for (int y = 0; y < levelHeight; y++)
        {
            for (int x = 0; x < levelWidth; x++)
            {
                if (!loadedRooms.Contains(new Vector2(x, y)))
                {
                    int rand = Random.Range(0, fillerRooms.Length);
                    transform.position = new Vector2(x, -y);
                    CreateRoom(fillerRooms[rand]);
                }
            }
        }
    }

    private GameObject GetRoom(Vector2 pos)
    {
        return roomArray[(int)pos.x, -(int)pos.y];
    }
}