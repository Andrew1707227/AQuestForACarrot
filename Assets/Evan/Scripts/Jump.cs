using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    //Holds speed of a jump
    public float jumpVelocity;
    //Holds fall speed up
    public float fallMultiplier = 2.5f;
    //Holds low jump fall speed up
    public float lowJumpMultiplier = 2f;
    //Holds if player is on ground
    public bool grounded;
    //Holds if the player is trying to jump
    private bool jumpCashed = false;
    //Holds hold time for cashed jumps
    public float cashedTime;
    //Times out based on cashedTome
    private float cashedTimer;
    //Holds length of coyote time
    public float coyoteTime = 0.2f;
    //Times coyotr time
    private float coyoteTimer = 0;

    private Rigidbody2D rb2;
    private SideMove sideMove;

    private void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        cashedTimer = cashedTime;

        //Get SideMove from player
        sideMove = gameObject.GetComponent<SideMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpCashed = true;
        }

        //Increments coyoteTimer
        if (coyoteTimer > 0)
        {
            coyoteTimer -= Time.deltaTime;
        }

        //Takes cashed jump input and adds upwards velocity if player is on ground or coyote timer is on
        if (jumpCashed == true && (grounded == true || coyoteTimer > 0))
        {
            rb2.velocity = new Vector2(rb2.velocity.x, jumpVelocity);

            //removes cashed jump
            jumpCashed = false;
        }
        else if (jumpCashed == true && cashedTimer > 0)
        {
            cashedTimer = cashedTimer - Time.deltaTime;
        }
        else if (jumpCashed == true && cashedTimer < 0)
        {
            jumpCashed = false;
            cashedTimer = cashedTime;
        }


        //Speeds up normal fall or low jump fall
            if (rb2.velocity.y < 0 && !grounded)
        {
            rb2.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb2.velocity.y > 0 && !Input.GetButton("Jump") && !grounded)
        {
            rb2.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (rb2.velocity.y > 0)
        {
            grounded = false;
        }
        else
        {
            //Starts coyote time
            coyoteTimer = coyoteTime;

            grounded = false;
        }
    }
}
