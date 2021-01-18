using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNOWANDBALLSWITHLOGIC : MonoBehaviour
{

    public float airTime;
    private Vector2 force;
    public float scalar;

    private float timer;
    private Rigidbody2D rb2;

    public GameObject player;
    public GameObject Baller;
    private SpriteRenderer sr;

    private bool direction;
    public Vector3 offset; //So the snowball is in player's hand
    private ParticleSystem PS;

    public float offMap;
    private bool isHit = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 8 /* 8 = ground layer */ || collision.tag == "Player") && !isHit)
        {
            isHit = true;
            GetComponent<AudioSource>().Play();
            PS.Play();
            rb2.velocity = new Vector2();
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(GetComponent<TrailRenderer>());
        }
    }

    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        sr = player.GetComponent<SpriteRenderer>();
        PS = GetComponent<ParticleSystem>();

        Vector3 playerPos = player.transform.position;
        Vector3 BallerPos = Baller.transform.position;

        direction = sr.flipX;
        timer = 0;

        if (gameObject.name == "SnowballClone")
        {
            rb2.gravityScale = 1;
            if (!direction)
            {
                transform.position = BallerPos + new Vector3(-offset.x, offset.y, offset.z);
            }
            else
            {
                transform.position = BallerPos + offset;
            }
            force = (playerPos - BallerPos);
            force = force.normalized * scalar;
            rb2.AddForce(force, ForceMode2D.Impulse);

        }
    }

    void Update()
    {
        if (gameObject.name == "SnowballClone")
        {
            timer += Time.deltaTime;
            if (timer < airTime)
            {
                // rb2.AddForce(force, ForceMode2D.Impulse);
            }
            if (transform.position.y < offMap)
            {
                Destroy(gameObject);
            }
        }
    }
}
