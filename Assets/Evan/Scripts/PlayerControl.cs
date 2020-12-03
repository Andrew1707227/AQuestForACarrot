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
    //Holds old downwards slope angle
    private float slopeDownAngleOld;
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
        rb2 = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        bc2 = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        CheckGround();
        ApplyMovement();
        SlopeCheck();
    }

    private void SlopeCheck()
    {
        SlopeCheckVertical(checkPos.position);
        SlopeCheckHorizontal(checkPos.position);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, Vector2.right, slopeCheckDistance, whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -Vector2.right, slopeCheckDistance, whatIsGround);

        if (slopeHitFront)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }

    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsGround);
        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != slopeDownAngleOld)
            {
                isOnSlope = true;
            }

            slopeDownAngleOld = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }

        Debug.Log(slopeSideAngle);
        if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
        {
            canWalkOnSlope = false;
        }
        else
        {
            canWalkOnSlope = true;
        }

        if (isOnSlope && xInput == 0.0f && canWalkOnSlope)
        {
            rb2.sharedMaterial = fullFriction;
        }
        else
        {
            rb2.sharedMaterial = noFriction;
        }
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (xInput == -facingDirection)
        {
            facingDirection *= -1;
            sr.flipX = !sr.flipX;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (canJump)
        {
            canJump = false;
            isJumping = true;
            newVelocity.Set(0.0f, 0.0f);
            rb2.velocity = newVelocity;
            newForce.Set(0.0f, jumpForce);
            rb2.AddForce(newForce, ForceMode2D.Impulse);
        }
    }

    private void ApplyMovement()
    {
        if (grounded && !isOnSlope && !isJumping)
        {
            newVelocity.Set(movementSpeed * xInput, 0.0f);
            rb2.velocity = newVelocity;
        }
        else if (grounded && isOnSlope && !isJumping && canWalkOnSlope)
        {
            newVelocity.Set(movementSpeed * slopeNormalPerp.x *-xInput, movementSpeed * slopeNormalPerp.y * -xInput);
            rb2.velocity = newVelocity;
        }
        else if (!grounded)
        {
            newVelocity.Set(movementSpeed * xInput, rb2.velocity.y);
            rb2.velocity = newVelocity;
        }
    }

    private void CheckGround()
    {
        grounded = Physics2D.OverlapCircle(groundCheckTrans.position, groundCheckRadius, whatIsGround);

        if (rb2.velocity.y <= 0.0f)
        {
            isJumping = false;
        }

        if (grounded && !isJumping && slopeDownAngle <= maxSlopeAngle)
        {
            canJump = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckTrans.position, groundCheckRadius);
    }
}
