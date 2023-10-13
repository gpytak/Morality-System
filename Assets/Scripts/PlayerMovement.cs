using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRb; // Player rigid body
    public SpriteRenderer spriteRenderer;
    public float speed;
    public float input;
    public float jumpForce;

    public LayerMask groundLayer; // Determine if player is on the ground
    private bool isGrounded;
    public Transform feetPosition;
    public float groundCheckCircle;

    public float jumpTime = 0.35f;
    public float jumpTimeCounter;
    private bool isJumping;
    
    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal"); // Inverses character
        if (input < 0) {
            spriteRenderer.flipX = true;
        } else if (input > 0) {
            spriteRenderer.flipX = false;
        }

        isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer); // Returns T/F if player is on ground

        if (Input.GetButtonDown("Jump") && isGrounded == true) { // Adds jump
            isJumping = true;
            jumpTimeCounter = jumpTime;
            playerRb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetButton("Jump") && isJumping == true) { // Hold jump
            if(jumpTimeCounter > 0) {
                playerRb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }

        if (Input.GetButton("Jump")) { // If jump is held
            isJumping = false;
        }
    }

    void FixedUpdate()
    {
        playerRb.velocity = new Vector2 (input * speed, playerRb.velocity.y);
    }
}
