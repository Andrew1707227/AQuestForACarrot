using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    //Holds current diraction moving
    private bool movingLeft = true;

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
        
    }
}
