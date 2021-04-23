using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] tileSpawns;

    private void Start()
    {
        int rand = Random.Range(0, tileSpawns.Length);
        if (tileSpawns[rand] != null)
            Instantiate(tileSpawns[rand], transform);
    }
}
