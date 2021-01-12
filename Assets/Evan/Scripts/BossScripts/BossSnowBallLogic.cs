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

    //Holds force to throw with
    private Vector2 force;

    //Holds if the snowball has hit something
    private bool isHit = false;

    //Component References
    private Rigidbody2D rb2;
    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        //Get component References 
        rb2 = GetComponent<Rigidbody2D>();
        ps = GetComponent<ParticleSystem>();

        //Gets position of boss and player/targetplayer
        Vector2 bossPos = boss.transform.position;
        Vector2 targetPlayerPos = new Vector2(player.transform.position.x, (player.transform.position.y + 7) + Random.Range(-1, 1));
        Vector2 playerPos = player.transform.position;

        //Gets force to go from boss to player
        force = (targetPlayerPos - bossPos) * scalar;

        //if clone throw
        if (gameObject.name == "BossSnowballCloneThrow")
        {
            rb2.gravityScale = 1;

            //Sets snowball postion to boss's hand
            transform.position = bossPos + offset;

            //Lanches snowball with force
            rb2.AddForce(force, ForceMode2D.Impulse);
        }

        //if clone drop
        if (gameObject.name == "BossSnowballCloneDrop" || gameObject.name == "BossSnowballCloneDrop2")
        {
            //Sets scale down 
            gameObject.transform.localScale = new Vector3(0, 0, 1);

            //if original clone drop
            if (gameObject.name == "BossSnowballCloneDrop")
            {
                //Spawn a clone drop and change its name
                GameObject dropSnowball = Instantiate(gameObject);
                dropSnowball.name = "BossSnowballCloneDrop2";
                //Set clone to postionELeft of original
                dropSnowball.transform.position = new Vector2(playerPos.x, bossPos.x + 4) + new Vector2(Random.Range(-3f, -1f), 0);

                //Spawn another clone drop and change its name
                GameObject dropSnowball2 = Instantiate(gameObject);
                dropSnowball2.name = "BossSnowballCloneDrop2";
                //Set clone to postion right of original
                dropSnowball2.transform.position = new Vector2(playerPos.x, bossPos.x + 4) + new Vector2(Random.Range(3f, 1f), 0);

                //Sets snowball postion to above player
                transform.position = new Vector2(playerPos.x, bossPos.x + 4);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if clone drop
        if (gameObject.name == "BossSnowballCloneDrop" || gameObject.name == "BossSnowballCloneDrop2")
        {
            if (gameObject.transform.localScale != new Vector3(0.5f, 0.5f, 1))
            {
                gameObject.transform.localScale += new Vector3(0.005f, 0.005f, 0);
            }
            else
            {
                rb2.gravityScale = 1;
            }
        }

            //if clone
            if (gameObject.name == "BossSnowballCloneThrow" || gameObject.name == "BossSnowballCloneDrop" || gameObject.name == "BossSnowballCloneDrop2")
        {
            //If offmap destroy this object
            if (transform.position.y < offMap)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 8 /* 8 = ground layer */ || collision.tag == "Player") && !isHit)
        {
            isHit = true;
            GetComponent<AudioSource>().Play();
            ps.Play();
            rb2.velocity = new Vector2(0 , 0);
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(GetComponent<TrailRenderer>());
        }
    }
}
