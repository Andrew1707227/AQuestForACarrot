using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMove : MonoBehaviour
{
    //Holds speed
    public float accel = 8;
    //Holds max speed
    public float maxSpeed = 4;
    //Holds slow down speed (Bigger = slower)
    public int slowDownSpeed = 5;

    private Rigidbody2D rb2;
    private SpriteRenderer sr;
    private Jump jump;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        jump = GetComponent<Jump>();
    }

    private void FixedUpdate()
    {
        Debug.Log(rb2.velocity.y);
        //Move right
        if (Input.GetKey("d"))
        {
            //Flip character
            sr.flipX = false;

            //Moves player
            if (rb2.velocity.x < 0)
            {
                rb2.AddForce(new Vector2(accel / 0.5f, 0));
            }
            else
            {
                rb2.AddForce(new Vector2(accel, 0));
            }

            //Speed cap
            if (rb2.velocity.x > maxSpeed)
            {
                rb2.velocity = new Vector2(maxSpeed, rb2.velocity.y);
            }
        }

        //Move left
        if (Input.GetKey("a"))
        {
            //Flip character
            sr.flipX = true;

            //Moves player
            if (rb2.velocity.x > 0)
            {
                rb2.AddForce(new Vector2(-accel / 2, 0));
            }
            else
            {
                rb2.AddForce(new Vector2(-accel, 0));
            }
            
            //Speed cap
            if (rb2.velocity.x < -maxSpeed)
            {
                rb2.velocity = new Vector2(-maxSpeed, rb2.velocity.y);
            }
        }

        //Handles x slow down (constantly active)
        if (rb2.velocity.x > 0)
        {
            if (rb2.velocity.x < 0.2)
            {
                rb2.velocity = new Vector2(0, rb2.velocity.y);
            }
            else
            {
                rb2.velocity = new Vector2(rb2.velocity.x - (rb2.velocity.x / 5), rb2.velocity.y);
            }
        }

        if (rb2.velocity.x < 0)
        {
            if (rb2.velocity.x > -0.2)
            {
                rb2.velocity = new Vector2(0, rb2.velocity.y);
            }
            else
            {
                rb2.velocity = new Vector2(rb2.velocity.x + (Mathf.Abs(rb2.velocity.x) / 5), rb2.velocity.y);
            }
        }
        /*
        //Handles y slow down for slopes(only active if not moving)
        if (!Input.GetKey("a") && !Input.GetKey("d"))
        {
            if (jump.grounded)
            {
                if (rb2.velocity.y < 0)
                {

                    if (rb2.velocity.x > -0.2)
                    {
                        rb2.velocity = new Vector2(rb2.velocity.x, 0);
                    }
                    else
                    {
                        rb2.velocity = new Vector2(rb2.velocity.x, rb2.velocity.y + (Mathf.Abs(rb2.velocity.y) / 2));
                    }
                }
            }
        }
        */
    }

}
