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
        if (movingLeft)
        {
            move(Vector2.left * 1.1f);
        }


        if (!movingLeft)
        {
            move(-Vector2.left * 1.1f);
        }
    }

    private void move(Vector2 moveValue)
    {
        rb2.velocity = moveValue;
        curMoveTime += Time.deltaTime;

        if (curMoveTime >= moveTime)
        {
            movingLeft = !movingLeft;
            curMoveTime = 0;
        }
    }
}
