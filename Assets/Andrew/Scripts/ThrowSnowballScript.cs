using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSnowballScript : MonoBehaviour {

    public GameObject SnowballTemplate;
    public Vector3 offset; //So the snowball is in player's hand

    public float CoolDown;
    private float TimeRemaining;

    public float forceCap;
   
    // Start is called before the first frame update
    void Start() {
        SnowballTemplate.transform.position = new Vector3(9999, 9999); //im not a bad coder you are
        SnowballTemplate.GetComponent<Rigidbody2D>().gravityScale = 0;
        TimeRemaining = CoolDown;
    }

    // Update is called once per frame
    void Update() {
        if (TimeRemaining <= CoolDown) TimeRemaining += Time.deltaTime;
        
        if (Input.GetButton("Fire1") && TimeRemaining >= CoolDown) {
            TimeRemaining = 0;
            GetComponent<AudioSource>().Play();
            GameObject Snowball = Instantiate(SnowballTemplate);
            Snowball.transform.position = transform.position + offset;
            Snowball.name = "SnowballClone";
            Rigidbody2D rb2 = Snowball.GetComponent<Rigidbody2D>();
            rb2.gravityScale = 1;
            rb2.AddForce(new Vector2(forceCap,forceCap));
        }
    }
}
