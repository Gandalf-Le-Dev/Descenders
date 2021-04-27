using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BorderGenerator : MonoBehaviour
{
    private int scale;
    private int width;
    private int height;

    private Vector3 direction = Vector3.right;

    [SerializeField] private LevelGeneration levelGenerator;
    
    [SerializeField] private Transform posStart;
    [SerializeField] private Transform posTR;
    [SerializeField] private Transform posBL;
    [SerializeField] private Transform posBR;
    [SerializeField] private Transform posEnd;

    [SerializeField] private Transform borderContainer;
    [SerializeField] private GameObject borderObject;

    private void Start()
    {
        /*
         * Get levelGenerator values
         */
        scale = levelGenerator.getScale();
        height = levelGenerator.getLevelHeigth();
        width = levelGenerator.getLevelWidth();
        
        /*
         * Setup corner transform
         */
        posStart.position = new Vector2(-1, 1);
        posTR.position = new Vector2(width * scale, 1);
        posBL.position = new Vector2(-1, - (height * scale));
        posBR.position = new Vector2(width * scale, - (height * scale));
        
        transform.position = posStart.position;
        posEnd.position = new Vector2(posStart.position.x, posStart.position.y - 1);
    }

    private void Update()
    {
        // if (transform.position == posTR.position)
        // {
        //     direction = Vector3.down;
        // }
        //
        // if (transform.position == posBR.position)
        // {
        //     direction = Vector3.left;
        // }
        //
        // if (transform.position == posBL.position)
        // {
        //     direction = Vector3.up;
        // }
        //
        // if (transform.position == posEnd.position)
        // {
        //     Destroy(gameObject);
        // }
        
        // moveBorderGenerator();
        
    }

    // private void moveBorderGenerator()
    // {
    //     transform.position += direction;
    //     Instantiate(borderObject, transform.position, Quaternion.identity, borderContainer);
    // }
}
