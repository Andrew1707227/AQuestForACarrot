using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public GameObject Enemy;
    private GameObject Snowball;
    private GameObject Player;
    //To check out where its going
    bool movingDirection = false;

    //Goomba Speed
    [SerializeField] float speed;
    void Update()
    {
        transform.Translate(((movingDirection ? -1 : 1) * Vector3.left * Time.deltaTime * speed));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Edge" || other.gameObject.tag == "Enemy")
        {
            //Reverse Direction please
            movingDirection = !movingDirection;
        }
    }
}