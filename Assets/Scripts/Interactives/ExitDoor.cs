using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private GameObject levelGenerator;
    private Tilemap tilemap;

    private void OnTriggerEnter2D(Collider2D other)
    {
        NextLevel();
    }

    public void NextLevel()
    {
        Destroy(FindObjectOfType<LevelGeneration>());
        Instantiate(levelGenerator, transform.root.parent);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            NextLevel();
        }
    }
}
