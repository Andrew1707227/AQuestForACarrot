using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    private void Start()
    {

    
    }
    public GameObject Enemy;
    private GameObject Snowball;
    private GameObject Player;
    //To check out where its going
    bool movingDirection = false;
    public float Duration;
    public Vector2 freeze = new Vector2(1, 2);

    //Goomba Speed
    [SerializeField] float Distance;
    void Update()
    {
        //transform.Translate(((movingDirection ? -1 : 1) * Vector3.left * Time.deltaTime * speed));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Edge" || other.tag == "Enemy")
        {
            //Reverse Direction please
            movingDirection = !movingDirection;
            //i like bread
        }
    }
}