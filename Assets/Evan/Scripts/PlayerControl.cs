using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //--Serialized floats
    //Holds speed
    [SerializeField]
    private float movementSpeed = 4;
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
    //Holds time to slow down for
    [SerializeField]
    private float slowDownTime;
    //Holds fall multiplyer
    [SerializeField]
    public float fallMultiplier;

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
    //Holds last xInput
    private float oldxInput;
    //Holds downwards slope angle
    private float slopeDownAngle;
    //Holds sideways slope angle
    private float slopeSideAngle;
    //Holds current turn around times count for left
    private float turnAroundTimerLeft = 0.01f;
    //Holds current turn around times count for right
    private float turnAroundTimerRight = 0.01f;

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
    //Holds if the player is in turn around slow left
    private bool turnAroundSlowLeft = false;
    //Holds if the player is in turn around slow right
    private bool turnAroundSlowRight = false;
    //Holds if the aniMoving should be true regardless of speed
    private bool aniMovingBuffer = false;

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
        Debug.Log(rb2.velocity.x);

        //Activate input check
        CheckInput();
    }

    private void FixedUpdate()
    {
        //Activate the ground check, movment, slope check, and the two managers
        CheckGround();
        GetMovement();
        SlopeCheck();
        DownSpeedManager();
        animationManager();

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

    private void animationManager()
    {
        //Checks if moving for animator
        if (rb2.velocity.x != 0.0f || aniMovingBuffer)
        {
            ani.SetBool("aniMoving", true);
            aniMovingBuffer = false;
        }
        else
        {
            ani.SetBool("aniMoving", false);
        }

        //Checks if jumping for animator
        if (!grounded && rb2.velocity.y > 0.0f)
        {
            ani.SetBool("aniGoingUp", true);
            ani.SetBool("aniGoingDown", false);

        }
        else if (!grounded && rb2.velocity.y < 0.0f)
        {
            ani.SetBool("aniGoingDown", true);
            ani.SetBool("aniGoingUp", false);
        }
        else
        {
            ani.SetBool("aniGoingUp", false);
            ani.SetBool("aniGoingDown", false);
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

            //Turns on aniMovingBuffer if moving
            if (rb2.velocity.x != 0.0f)
            {
                aniMovingBuffer = true;
            }

            //Toggles can jump and is jumping
            canJump = false;
            isJumping = true;


            //Checks for upwards movement
            if (rb2.velocity.y > 0)
            {    

                //Sets x velocity to 0
                newVelocity.Set(0.0f, rb2.velocity.y - (rb2.velocity.y / 4));
                rb2.velocity = newVelocity;
            }
            else
            {

                //Sets x and y velocity to 0
                newVelocity.Set(0.0f, 0.0f);
                rb2.velocity = newVelocity;
            }

            //Adds jump force
            newForce.Set(0.0f, jumpForce);
            rb2.AddForce(newForce, ForceMode2D.Impulse);
        }
    }

    private void GetMovement()
    {
        if ((grounded && !isOnSlope && !isJumping))
        {
            //Gets flat movement
            newVelocity.Set(movementSpeed * xInput, 0.0f);
            //Sends newVelocity to ApplyMovement
            ApplyMovement(newVelocity, false);
        }
        else if (grounded && isOnSlope && !isJumping && canWalkOnSlope)
        {
            //Gets slope movement
            newVelocity.Set(movementSpeed * slopeNormalPerp.x * -xInput, movementSpeed * slopeNormalPerp.y * -xInput);
            //Sends newVelocity to ApplyMovement
            ApplyMovement(newVelocity, true);
        }
        else if (grounded && isOnSlope && !isJumping && !canWalkOnSlope && highLeft)
        {
            //Gets Right only slope movement
            if (-xInput <= 0)
            {
                newVelocity.Set(movementSpeed * slopeNormalPerp.x * -xInput, movementSpeed * slopeNormalPerp.y * -xInput);
                //Sends newVelocity to ApplyMovement
                ApplyMovement(newVelocity, true);
            }
        }
        else if (grounded && isOnSlope && !isJumping && !canWalkOnSlope && highRight)
        {
            //Gets Left only slope movement
            if (-xInput >= 0)
            {
                newVelocity.Set(movementSpeed * slopeNormalPerp.x * -xInput, movementSpeed * slopeNormalPerp.y * -xInput);
                //Sends newVelocity to ApplyMovement
                ApplyMovement(newVelocity, true);
            }
        }
        else if (!grounded)
        {
            //Gets air movement
            newVelocity.Set(movementSpeed * xInput, rb2.velocity.y);
            //Sends newVelocity to ApplyMovement
            ApplyMovement(newVelocity, false);
        }
    }

    public void ApplyMovement(Vector2 velocityToUse, bool effectY)
    {
        //Checks for changes in xInput
        if ((oldxInput != xInput && xInput != 0.0f) && xInput == -1f)
        {
            //Turns on slow left
            turnAroundSlowLeft = true;

            //Turns off slow right
            turnAroundSlowRight = false;
            turnAroundTimerRight = 0.01f;
        }
        else if ((oldxInput != xInput && xInput != 0.0f) && xInput == 1f)
        {
            //Turns on slow right
            turnAroundSlowRight = true;

            //Turns off slow left
            turnAroundSlowLeft = false;
            turnAroundTimerLeft = 0.01f;
        }

        //Checks if slow down changes are needed
        if (turnAroundSlowLeft)
        {
            //Slows down x speed
            velocityToUse.Set(velocityToUse.x * (turnAroundTimerLeft / slowDownTime), velocityToUse.y);

            if (effectY)
            {
                //Slows down y speed
                velocityToUse.Set(velocityToUse.x , velocityToUse.y * (turnAroundTimerLeft / slowDownTime));
            }

            //Sets velocity to velocityToUse
            rb2.velocity = velocityToUse;

            //increments turnAroundCounter
            turnAroundTimerLeft += Time.deltaTime;

            //Checks if turnAroundTime is done
            if (turnAroundTimerLeft >= slowDownTime)
            {
                //Turns off slow turn around and resets timer
                turnAroundSlowLeft = false;
                turnAroundTimerLeft = 0.01f;
            }
        }
        else if (turnAroundSlowRight)
        {
            //Slows down x speed
            velocityToUse.Set(velocityToUse.x * (turnAroundTimerRight / slowDownTime), velocityToUse.y);

            if (effectY)
            {
                //Slows down y speed
                velocityToUse.Set(velocityToUse.x, velocityToUse.y * (turnAroundTimerRight / slowDownTime));
            }

            //Sets velocity to velocityToUse
            rb2.velocity = velocityToUse;

            //increments turnAroundCounter
            turnAroundTimerRight += Time.deltaTime;

            //Checks if turnAroundTime is done
            if (turnAroundTimerRight >= slowDownTime)
            {
                //Turns off slow turn around and resets timer
                turnAroundSlowRight = false;
                turnAroundTimerRight = 0.01f;
            }
        }
        else
        {
            //Debug.Log("Default");
            //Sets velocity to velocityToUse
            rb2.velocity = velocityToUse;
        }

        //Gets old xInput
        oldxInput = xInput;
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

    //Manages downaward speed
    private void DownSpeedManager()
    {
        //Speeds up normal fall or low jump fall
        if (rb2.velocity.y < 0 && !grounded)
        {
            rb2.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb2.velocity.y > 0 && !Input.GetButton("Jump") && !grounded)
        {
            rb2.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        //Draws ground detection sphere on screen
        Gizmos.DrawWireSphere(groundCheckTrans.position, groundCheckRadius);
    }
}
