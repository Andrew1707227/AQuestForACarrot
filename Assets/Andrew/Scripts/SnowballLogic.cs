using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballLogic : MonoBehaviour {

    public float airTime;
    public Vector2 force;

    private float timer;
    private Rigidbody2D rb2;

    public GameObject player;
    private SpriteRenderer sr;
    private bool direction;
    public Vector3 offset; //So the snowball is in player's hand

    public float offMap;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Enemy") {
            GameObject currEnemy = collision.gameObject;
            //kill the enemy
        }
        if (collision.tag != "Player") {
            Destroy(gameObject);
        }
    }

    void Start() {
        rb2 = GetComponent<Rigidbody2D>();
        sr = player.GetComponent<SpriteRenderer>();
        direction = sr.flipX;
        timer = 0;
        if (gameObject.name == "SnowballClone") {
            rb2.gravityScale = 1;
            if (!direction) {
                transform.position = player.transform.position + new Vector3(-offset.x,offset.y,offset.z);
            } else {
                transform.position = player.transform.position + offset;
            } 
        }
    }

    void Update() {
        if (gameObject.name == "SnowballClone") { 
            timer += Time.deltaTime;
            if (timer < airTime) {
                if (!direction) {
                    rb2.AddForce(new Vector2(-force.x, force.y));
                } else {
                    rb2.AddForce(force);
                }
            }
            if(transform.position.y < offMap) {
                Destroy(gameObject);
            }
        }
    }
}
