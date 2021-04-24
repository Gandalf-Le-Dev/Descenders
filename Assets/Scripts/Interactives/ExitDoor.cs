using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        NextLevel();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Game");
    }
}
