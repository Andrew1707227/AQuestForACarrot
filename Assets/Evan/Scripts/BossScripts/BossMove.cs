using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    //Holds current diraction moving
    private bool movingLeft = true;
    //Holds move time
    private float moveTime = 2;
    //Holds current move time
    private float curMoveTime = 0;

    //Component References
    private Rigidbody2D rb2;
    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        //Get component References
        rb2 = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //If moving left 
        if (movingLeft)
        {
            //Call move and give it a positive number
            move(Vector2.left * 1.1f);
        }

        //if moving right
        if (!movingLeft)
        {
            //Call move and give it a negative number
            move(-Vector2.left * 1.1f);
        }
    }

    private void move(Vector2 moveValue)
    {
        //If boss is not in yell animation
        if (BossStillStart.isStarted)
        {
            //Add movment to bunny
            rb2.velocity = moveValue;
        }

        //Increment currnet move time
        curMoveTime += Time.deltaTime;

        //If timer is greater than time wanted
        if (curMoveTime >= moveTime)
        {
            //Set moving left to the opposite of itself
            movingLeft = !movingLeft;
            //Reset curMoveTime
            curMoveTime = 0;
        }
    }
}
