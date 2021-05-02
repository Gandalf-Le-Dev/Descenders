using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TileSpawn : MonoBehaviour
{
    private Tilemap tilemap;
    [SerializeField] private TileBase tile;

    private void Start()
    {
        tilemap = FindObjectOfType<Tilemap>();

        tilemap.SetTile(tilemap.LocalToCell(transform.position), tile);


        // int rand = Random.Range(0, tileSpawns.Length);
        // if (tileSpawns[rand] != null)
        //     Instantiate(tileSpawns[rand], transform);
    }
}
