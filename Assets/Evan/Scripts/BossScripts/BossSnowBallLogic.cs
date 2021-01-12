using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSnowBallLogic : MonoBehaviour
{ 
    //Holds spawn offset
    [SerializeField]
    private Vector2 offset;
    //Holds what is offmap
    [SerializeField]
    private float offMap;
    //Holds force scaler
    [SerializeField]
    private float scalar;

    //Holds referece to boss gameobject
    [SerializeField]
    private GameObject boss;
    //Holds referece to player gameobject
    [SerializeField]
    private GameObject player;

    private Vector2 force;

    //Component References
    private Rigidbody2D rb2;
    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        //Get component References 
        rb2 = GetComponent<Rigidbody2D>();
        ps = GetComponent<ParticleSystem>();

        //Gets position of boss and player
        Vector2 bossPos = boss.transform.position;
        Vector2 playerPos = new Vector2(player.transform.position.x, (player.transform.position.y + 7) + Random.Range(-1,1));
        
        //Gets force to go from boss to player
        force = (playerPos - bossPos) * scalar;

        //if clone
        if (gameObject.name == "BossSnowballClone")
        {
            rb2.gravityScale = 1;

            //Sets snowball postion to boss's hand
            transform.position = bossPos + offset;

            //Lanches snowball with force
            rb2.AddForce(force, ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if clone
        if (gameObject.name == "BossSnowballClone")
        {
            //If offmap destroy this object
            if (transform.position.y < offMap)
            {
                Destroy(gameObject);
            }
        }
    }
}
