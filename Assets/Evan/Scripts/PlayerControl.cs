using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //--Serialized floats
    //Holds speed
    [SerializeField]
    private float movementSpeed = 4;
    //Holds max speed
    [SerializeField]
    private float maxSpeed = 4;
    //Holds jump force
    [SerializeField]
    private float jumpForce;
    //Holds radius for ground check sphere
    [SerializeField]
    private float groundCheckRadius;
    //Holds raycast distance for checking slopes
    [SerializeField]
    private float slopeCheckDistance;
    //Holds max slope angle the player can climb
    [SerializeField]
    private float maxSlopeAngle;

    //--Serialized Transforms
    //Holds loction for ground check sphere
    [SerializeField]
    private Transform groundCheckTrans;
    //Holds raycast check position
    [SerializeField]
    private Transform checkPos;

    //--Serialized LayerMasks
    //Holds ground layer reference
    [SerializeField]
    private LayerMask whatIsGround;

    //--Serialized PhysicsMaterial2D
    //Holds 0 friction physics material
    [SerializeField]
    private PhysicsMaterial2D noFriction;
    //Holds max friction physics material
    [SerializeField]
    private PhysicsMaterial2D fullFriction;


    //--Private floats
    //Holds x-input
    private float xInput;
    //Holds downwards slope angle
    private float slopeDownAngle;
    //Holds sideways slope angle
    private float slopeSideAngle;

    //--Private ints
    //Holds current facing (1 = right)
    private int facingDirection = 1;

    //--Private bools
    //Holds if player is grounded
    private bool grounded;
    //Holds if the player is on a slope
    private bool isOnSlope;
    //Holds if the player is jumping
    private bool isJumping;
    //Holds if the player can currently jump
    private bool canJump;
    //Holds if the player can walk on slope
    private bool canWalkOnSlope;
    //Holds if to high slope to the left
    private bool highLeft;
    //Holds if to high slope to the right
    private bool highRight;

    //--Private Vector2s
    //Temporally Holds new velocitys
    private Vector2 newVelocity;
    //Temporally Holds new forces
    private Vector2 newForce;
    //Holds the vector of the slope's surface
    private Vector2 slopeNormalPerp;

    //Component References
    private Rigidbody2D rb2;
    private SpriteRenderer sr;
    private Animator ani;
    private BoxCollider2D bc2;
    

    // Start is called before the first frame update
    void Start()
    {
        //Get component References
        rb2 = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        bc2 = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Activate input check
        CheckInput();
    }

    private void FixedUpdate()
    {
        //Activate the ground check, movment, and slope check
        CheckGround();
        ApplyMovement();
        SlopeCheck();

        //Checks if slope angle is a slope
        if (slopeDownAngle != 0.0f || slopeSideAngle != 0.0f)
        {
            isOnSlope = true;
        }
        else
        {
            isOnSlope = false;
        }
    }

    private void SlopeCheck()
    {
        //Activate Vertical and Horizontal checks and give them a starting point
        SlopeCheckVertical(checkPos.position);
        SlopeCheckHorizontal(checkPos.position);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        //Sends two rays out, one right one left
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, Vector2.right, slopeCheckDistance, whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -Vector2.right, slopeCheckDistance, whatIsGround);

        if (slopeHitFront)
        {
            //If hit right, calculate the side angle
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack)
        {
            //If hit left, calculate the side angle
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            //If no hit, set side angle to 0
            slopeSideAngle = 0.0f;
        }

        //Toggles highRight
        if (slopeHitFront)
        {
            highRight = true;
        }
        else 
        {
            highRight = false;
        }

        //Toggles highLeft
        if (slopeHitBack)
        {
            highLeft = true;
        }
        else
        {
            highLeft = false;
        }

    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        //Sends ray down
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsGround);

        //If something is hit by the ray
        if (hit)
        {
            //Get the Vertor of the slopes surface
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            //Get the angle of the slope
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            //Draws ray and slope Vector
            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }

        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }

        if (isOnSlope && xInput == 0.0f) // && canWalkOnSlope)
        {
            rb2.sharedMaterial = fullFriction;
        }
        else
        {
            rb2.sharedMaterial = noFriction;
        }
    }

    private void Jump()
    {
        //Jumps if canJump is true
        if (canJump)
        {
            //Toggles can jump and is jumping
            canJump = false;
            isJumping = true;

            //Sets x and y velocity to 0
            newVelocity.Set(0.0f, 0.0f);
            rb2.velocity = newVelocity;
            //Adds jump force
            newForce.Set(0.0f, jumpForce);
            rb2.AddForce(newForce, ForceMode2D.Impulse);
        }
    }

    private void ApplyMovement()
    {
        if ((grounded && !isOnSlope && !isJumping))
        {
            //Applies flat movement
            newVelocity.Set(movementSpeed * xInput, 0.0f);
            rb2.velocity = newVelocity;
        }
        else if (grounded && isOnSlope && !isJumping && canWalkOnSlope)
        {
            //Applies slope movement
            newVelocity.Set(movementSpeed * slopeNormalPerp.x * -xInput, movementSpeed * slopeNormalPerp.y * -xInput);
            rb2.velocity = newVelocity;
        }
        else if (grounded && isOnSlope && !isJumping && !canWalkOnSlope && highLeft)
        {
            //Applies Right only slope movement
            if (-xInput <= 0)
            {
                newVelocity.Set(movementSpeed * slopeNormalPerp.x * -xInput, movementSpeed * slopeNormalPerp.y * -xInput);
                rb2.velocity = newVelocity;
            }
        }
        else if (grounded && isOnSlope && !isJumping && !canWalkOnSlope && highRight)
        {
            //Applies Left only slope movement
            if (-xInput >= 0)
            {
                newVelocity.Set(movementSpeed * slopeNormalPerp.x * -xInput, movementSpeed * slopeNormalPerp.y * -xInput);
                rb2.velocity = newVelocity;
            }
        }
        else if (!grounded)
        {
            //Applies air movement
            newVelocity.Set(movementSpeed * xInput, rb2.velocity.y);
            rb2.velocity = newVelocity;
        }
    }

    private void CheckGround()
    {
        //Checks for ground on LayerMask layer within a sphere
        grounded = Physics2D.OverlapCircle(groundCheckTrans.position, groundCheckRadius, whatIsGround);

        //If they player can jump and is falling or grounded reset is jumping
        if (canJump)
        {
            if (rb2.velocity.y <= 0.0f || grounded)
            {
                isJumping = false;
            }
        }

        //Toggles can jump
        if (grounded && slopeDownAngle <= maxSlopeAngle)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

    }

    //Checks for inputs
    private void CheckInput()
    {
        //Logs xInput to varible
        xInput = Input.GetAxisRaw("Horizontal");

        //Flips player
        if (xInput == -facingDirection)
        {
            facingDirection *= -1;
            sr.flipX = !sr.flipX;
        }

        //Jumps when jump is pressed
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void OnDrawGizmos()
    {
        //Draws ground detection sphere on screen
        Gizmos.DrawWireSphere(groundCheckTrans.position, groundCheckRadius);
    }
}
