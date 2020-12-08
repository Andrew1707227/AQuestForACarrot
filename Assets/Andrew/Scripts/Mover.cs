using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
    // Start is called before the first frame update

    public float speed = 8;
    private Rigidbody2D rb2;
    private SpriteRenderer sr;

    void Start() {
        rb2 = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate() {
        rb2.velocity = new Vector2(Input.GetAxis("Horizontal") * speed,rb2.velocity.y);
        if (Input.GetAxis("Horizontal") < 0) {
            sr.flipX = false;
        } else if (Input.GetAxis("Horizontal") > 0) {
            sr.flipX = true;
        }
    }
}