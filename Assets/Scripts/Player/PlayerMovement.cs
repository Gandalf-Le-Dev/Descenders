using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerMovement : PlayerController
{
    [SerializeField] private int playerSpeed;

    [SerializeField] private int playerJumpHeight;

    private bool facingRight = true;

    private void Update()
    {
        UpdatePlayer();
        ChangeCam();
    }

    private void UpdatePlayer()
    {
        UpdatePlayerFacing();
        Crouch();
        // TODO: Convert to the new input system
        float pSpeed = Input.GetAxis("Horizontal");

        Vector2 move = Vector2.zero;

        // Move
        if (!crouched && !Input.GetKey(KeyCode.LeftControl))
            move.x = Input.GetAxis("Horizontal");
        // Sprint
        else if(!crouched && Input.GetKey(KeyCode.LeftControl))
            move.x = Input.GetAxis("Horizontal") * 2;
        // Jump
        if (Input.GetButton("Jump") && grounded)
        {
            if (Mathf.Abs(velocity.x) > 0)
            {
                velocity.y = playerJumpHeight + Mathf.Abs(velocity.x * 0.2f);
            }
            else
            {
                velocity.y = playerJumpHeight;
            }
        }
        else if (Input.GetButtonDown("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y *= 0.5f;
            }
        }
        else
        {
            crouched = false;
        }

        if (velocity.x > 0)
            facingRight = true;
        else if (velocity.x < 0) 
            facingRight = false;

        targetVelocity = move * playerSpeed;
    }
    
    private void UpdatePlayerFacing()
    {
        if (facingRight) transform.localScale = new Vector3(1, 1, 1);
        if (facingRight == false) transform.localScale = new Vector3(-1, 1, 1);
    }

    private void ChangeCam()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            mainCamera.m_Lens.OrthographicSize = 7.5f;
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            mainCamera.m_Lens.OrthographicSize = 25f;
        }
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            mainCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset =
                new Vector3(0, -7, 0);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            mainCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset =
                Vector3.zero;
        }
    }
}