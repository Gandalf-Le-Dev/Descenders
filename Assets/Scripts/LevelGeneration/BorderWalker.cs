using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderWalker : MonoBehaviour
{
    [SerializeField] private Transform borderContainer;
    [SerializeField] private GameObject borderObject;

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
        if (direction == Direction.Up)
        {
            walkDirection = Vector2.up;
            drawDirectionLine(walkDirection);
        }
        
        if (direction == Direction.Down)
        {
            walkDirection = Vector2.down;
            drawDirectionLine(walkDirection);
        }
        
        if (direction == Direction.Left)
        {
            walkDirection = Vector2.left;
            drawDirectionLine(walkDirection);
        }
        
        if (direction == Direction.Right)
        {
            walkDirection = Vector2.right;
            drawDirectionLine(walkDirection);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += walkDirection;
        Instantiate(borderObject, transform.position, Quaternion.identity, borderContainer);
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

