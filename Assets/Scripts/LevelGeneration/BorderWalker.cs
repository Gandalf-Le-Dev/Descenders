using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderWalker : MonoBehaviour
{
    [SerializeField] private LevelGeneration levelGenerator;

    [SerializeField] private Transform borderContainer;
    [SerializeField] private GameObject borderObject;

    /*
     * Move this to a singleton ?
     */
    [SerializeField] private Transform posTL;
    [SerializeField] private Transform posTR;
    [SerializeField] private Transform posBL;
    [SerializeField] private Transform posBR;

    private int scale;
    private int width;
    private int height;

    private Vector3 walkDirection;

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    };

    public Direction direction;

    void Start()
    {
        /*
         * Check directions
         */
        if (direction == Direction.Up)
        {
            transform.position = posBL.position;
            walkDirection = Vector2.up;
            drawDirectionLine(walkDirection);
        }

        if (direction == Direction.Down)
        {
            transform.position = posTR.position;
            walkDirection = Vector2.down;
            drawDirectionLine(walkDirection);
        }

        if (direction == Direction.Left)
        {
            transform.position = posBR.position;
            walkDirection = Vector2.left;
            drawDirectionLine(walkDirection);
        }

        if (direction == Direction.Right)
        {
            transform.position = posTL.position;
            walkDirection = Vector2.right;
            drawDirectionLine(walkDirection);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += walkDirection;
        DontDestroyOnLoad(Instantiate(borderObject, transform.position, Quaternion.identity, borderContainer));

        /*
         * Check position to destroy object
         */
        if (transform.position == posTL.position || transform.position == posTR.position ||
            transform.position == posBL.position || transform.position == posBR.position)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If collide with anther border walker destroy itself
        if (other.GetComponent<BorderWalker>())
        {
            Destroy(gameObject);
        }
    }

    private void drawDirectionLine(Vector3 dir)
    {
        Vector3 pos = transform.position;
        Vector3 pos2;
        pos2 = pos + (dir * 10);
        Debug.DrawLine(pos, pos2);
    }
}