using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public GameObject Enemy;
    private GameObject Snowball;

    //To check out where its going
    bool movingDirection = false;

    //Goomba Speed
    [SerializeField] float speed;
    void Update()
    {
        transform.Translate(((movingDirection ? -1 : 1) * Vector3.left * Time.deltaTime * speed));
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Edge" || other.gameObject.tag == "Enemy")
        {
            //Reverse Direction please
            movingDirection = !movingDirection;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Being Killed
            Destroy(this.gameObject);
            //AddScore
        }
    }
}
