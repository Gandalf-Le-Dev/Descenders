using System;
using Cinemachine;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private float movementInputDirection;

        private int amountJumpLeft;

        private bool isFacingRight = true;
        private bool isWalking = false;
        private bool isGrounded = false;
        private bool canJump = true;
        private bool isTouchingWall = false;
        private bool isWallSliding = false;

        private Rigidbody2D rb;
        private Animator anim;
        private CinemachineVirtualCamera mainCamera;

        [SerializeField] private float movementSpeed = 10.0f;
        [SerializeField] private float jumpForce = 16.0f;
        [SerializeField] private float groundCheckRadius = 1f;
        [SerializeField] private float wallCheckDistance = 1f;
        [SerializeField] private float wallSlideSpeed = 1f;

        [SerializeField] private int maxJumps = 1;

        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform wallCheck;

        [SerializeField] private LayerMask groundMask;

        private void OnEnable()
        {
            mainCamera = FindObjectOfType<CinemachineVirtualCamera>();
            mainCamera.m_Follow = transform;
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            amountJumpLeft = maxJumps;
        }

        private void Update()
        {
            // Check
            CheckInputs();
            CheckMovementDirection();
            CheckCanJump();
            CheckWallSlide();

            // Update
            UpdateAnimations();
        }

        private void FixedUpdate()
        {
            ApplyMovement();
            CheckSurroundings();
        }

        private void CheckSurroundings()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);
            isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, groundMask);
        }

        private void CheckInputs()
        {
            movementInputDirection = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }

        /*
         * MOVEMENT
         */
        private void ApplyMovement()
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
            if (isWallSliding)
            {
                if (rb.velocity.y < -wallSlideSpeed)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
                }
            }
        }

        private void CheckMovementDirection()
        {
            // Flip the character
            if (isFacingRight && movementInputDirection < 0) Flip();
            else if (!isFacingRight && movementInputDirection > 0) Flip();

            // Update isWalking bool
            if (rb.velocity.x != 0) isWalking = true;
            else isWalking = false;
        }

        /*
         * ACTIONS
         */
        private void CheckCanJump()
        {
            if (isGrounded && rb.velocity.y <= 0)
            {
                amountJumpLeft = maxJumps;
            }

            if (amountJumpLeft <= 0)
            {
                canJump = false;
            }
            else
            {
                canJump = true;
            }
        }

        private void Jump()
        {
            if (canJump){
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                amountJumpLeft--;
            }
        }

        private void CheckWallSlide()
        {
            if (isTouchingWall && !isGrounded && rb.velocity.y <= 0)
            {
                isWallSliding = true;
            }
            else
            {
                isWallSliding = false;
            }
        }

        /*
         * MISCELLANEOUS
         */
        private void UpdateAnimations()
        {
            anim.SetBool("isWalking", isWalking);
            anim.SetBool("isGrounded", isGrounded);
            anim.SetBool("isWallSliding", isWallSliding);

            anim.SetFloat("yVelocity", rb.velocity.y);
        }

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        }
    }
}