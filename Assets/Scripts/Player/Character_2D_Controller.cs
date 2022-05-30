using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_2D_Controller : MonoBehaviour
{
    //PUBLIC
    public float movementSpeed = 5f;
    public float crouchSpeed = 3f;
    public float jumpForce = 6.5f;
    [Range(0.2f, 1f)] public float wallSlideSpeed = 0.5f; 
    public Collider2D crouchDisableCollider;
    public Transform groundCheckCollider;
    public Transform ceilingCheckCollider;
    public Transform wallCheckCollider;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public Animator animator;
    private Rigidbody2D _rigidbody;

    //CONST
    const float groundCheckRadius = 0.1f;
    const float ceilingCheckRadius = 0.2f;
    const float wallCheckRadius = 0.05f;
    const float maxJumps = 2f;

    //PLAYER MOVEMENT
    float horizontalMovement = 0f;
    float verticalMovement = 0f;
    bool playerFacingRight = true;

    //JUMPING
    float jumpsLeft = maxJumps;
    bool jumpFlag = false;
    bool isJumping = false;
    bool isFalling = false;

    //CROUCHING
    bool crouchFlag = false; //user let go of the crouch button
    bool isCrouching = false; //is user still crouching (example: blocked by another block so cant stand up)

    //COLLISION DETECTORS
    bool isGrounded = false;
    bool isTouchingCeiling = false;
    bool isTouchingWall = false;

    //WALL-SLIDING
    bool isWallSliding = false;
    bool isJumpingOffWall = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //user movement input
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        //checks if we are touching ground
        GroundCheck();

        //checks if we are touching ceiling
        CeilingCheck();

        //checks if we are touching wall
        WallCheck();

        //updates animator speed parameter we have set ourselves
        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));

        //jump button
        if (Input.GetButtonDown("Jump") && !isWallSliding)
        {
            //checks if player has jumps left
            //play jump animation
            if (jumpsLeft == maxJumps)
            {
                jumpFlag = true;
                animator.SetBool("isJumping", true);
            }
            //play double-jump animation
            else if (jumpsLeft > 0 && jumpsLeft < maxJumps)
            {
                jumpFlag = true;
                animator.SetBool("isDoubleJumping", true);
            }
        }

        //checks if the player landed and resets double jump
        if (isGrounded)
        {
            jumpsLeft = maxJumps;
        }

        //crouch button
        if (Input.GetButtonDown("Crouch"))
        {
            crouchFlag = true;
        }
        //crouch button release
        else if (Input.GetButtonUp("Crouch"))
        {
            crouchFlag = false;
        }
    }

    private void FixedUpdate()
    {
        //ALTERNATIVE: transform.position += new Vector3(horizontalMovement, 0, 0) * Time.fixedDeltaTime * movementSpeed;
        //move when crouched
        if (isCrouching)
            transform.Translate(Mathf.Abs(horizontalMovement * Time.fixedDeltaTime * crouchSpeed), 0, 0);
        //move when walking
        else
            transform.Translate(Mathf.Abs(horizontalMovement * Time.fixedDeltaTime * movementSpeed), 0, 0);

        //rotates player based on direction of the movement
        if ((horizontalMovement == -1 && playerFacingRight) || (horizontalMovement == 1 && !playerFacingRight))
            Flip();

        //player jumps if it has enough jumps
        if (jumpFlag == true)
        {
            //jump
            _rigidbody.velocity = new Vector2(0, jumpForce);
            jumpsLeft--;
            isGrounded = false;
            isJumping = true;
            jumpFlag = false;
        }

        //checks if player is grounded and disable jump animation
        if (isGrounded)
        {
            isJumping = false;

            //trigger animations
            animator.SetBool("isJumping", false);
            animator.SetBool("isDoubleJumping", false);
        }

        //checks if player is falling
        if (_rigidbody.velocity.y < 0 && !isGrounded)
            isFalling = true;
        else
            isFalling = false;

        //if player is falling 
        if (isFalling)
        {
            //tuns back to jump animation from double-jump animation
            animator.SetBool("isDoubleJumping", false);
            animator.SetBool("isJumping", true);
        }

        //removes colider when crouched
        if (crouchFlag && isGrounded)
        {
            if (crouchDisableCollider != null)
            {
                crouchDisableCollider.enabled = false;
                isCrouching = true;
            }
        }
        else if (!crouchFlag && !isTouchingCeiling && isCrouching)
        {
            if (crouchDisableCollider != null)
            {
                crouchDisableCollider.enabled = true;
                isCrouching = false;
            }
        }

        //wall-slide
        if (isTouchingWall && !isGrounded && Mathf.Abs(horizontalMovement) > 0)
        {
            isWallSliding = true;
            _rigidbody.velocity = new Vector2(0, -wallSlideSpeed);
            animator.SetBool("isWallSliding", true);

            //if wall sliding means its not jumping
            isJumping = false;

            //sets jumps to 1 because you can only single jump from a wall
            if(!isJumpingOffWall)
            {
                isJumpingOffWall = true;
                jumpsLeft = 1;
            }
        }
        else
        {
            isWallSliding = false;
            animator.SetBool("isWallSliding", false);
        }
    }

    //flips player in the y-axis
    private void Flip()
    {
        playerFacingRight = !playerFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    //checks if player is touching the ground
    private void GroundCheck()
    {
        isGrounded = false;
        //only check if the player is stationary in the y-axis since we dont have any slopes in the game (if you have slopes just check for velocity.y == 0 when you are jumping)
        if (_rigidbody.velocity.y == 0)
        {
            //if anything colides with that groundcheck radius we created it gets stored into colliders array
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
            if (colliders.Length > 0)
            {
                //we are grounded
                isGrounded = true;
                isJumpingOffWall = false;
                //Debug.Log("We are grounded!");
            }
        }
    }

    //checks if Player is touching the ceiling
    private void CeilingCheck()
    {
        isTouchingCeiling = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(ceilingCheckCollider.position, ceilingCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isTouchingCeiling = true;
            //Debug.Log("We are touching the Ceiling");
        }
    }

    //checks if Player is facing a wall
    private void WallCheck()
    {
        isTouchingWall = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(wallCheckCollider.position, wallCheckRadius, wallLayer);
        if (colliders.Length > 0)
        {
            isTouchingWall = true;
            //Debug.Log("touching wall!");
        }
    }
}

//maybe move isGrounded form fixed update to update