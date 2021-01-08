using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyAnimator : MonoBehaviour {

    private Animator anim;
    private SpriteRenderer sr;

    private float timeRemaining = 0;
    private Vector3 prevTransform;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()  {
        sr.flipX = transform.position.x > prevTransform.x;
        anim.Play("BunnyJump");
        prevTransform = transform.position;
    }
}
