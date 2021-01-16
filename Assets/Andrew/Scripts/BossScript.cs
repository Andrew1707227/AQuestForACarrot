using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

    public GameObject carrot;
    public GameObject Boss;
    public GameObject EvilWall;
    private PolygonCollider2D wallCollider;
    private SpriteRenderer wallSprite;

    private GameObject BossClone;
    private bool debounce;
    private Vector3 bossPos;

    private bool isDead;
    private bool isPlayerDead;
    private float timer;
    private float CarrotTimer;

    void Start() {
        carrot.SetActive(false);
        Boss.SetActive(false);
        isDead = false;
        wallSprite = EvilWall.GetComponent<SpriteRenderer>();
        wallCollider = EvilWall.GetComponent<PolygonCollider2D>();
        CarrotTimer = 0;
        timer = 0;
    }

    // Update is called once per frame
    void Update() {
        if (BossClone != null) { 
            if (BossClone.GetComponent<BossBunnyHealth>().getLives() <= 0) {
                isDead = true;
            } else {
                bossPos = BossClone.transform.position;
            }
        }
        if (isDead) CarrotTimer += Time.deltaTime;
        if (CarrotTimer > 2 && CarrotTimer < 2.1) {
            carrot.SetActive(true);
            carrot.transform.position = bossPos + new Vector3(-2,2,0);
        }
        if (PlayerHealthScript.GetLives() <= 0) {
            //reset the boss trigger
            isPlayerDead = true;
            if (BossClone != null) debounce = false;
        } else {
            isPlayerDead = false;
        }
        if (isPlayerDead && BossClone != null) timer += Time.deltaTime;
        if (timer > 2 && BossClone != null && !isPlayerDead) {
            Destroy(BossClone);
            timer = 0;
        }

        wallCollider.enabled = debounce;
        wallSprite.enabled = debounce;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && !debounce && !isPlayerDead) {
            GetComponent<AudioSource>().Play();
            debounce = true;
            BossClone = Instantiate(Boss);
            BossClone.name = "BossBun";
            BossClone.SetActive(true);
            BossClone.GetComponent<Animator>().Play("Boss_Yell");
        }
    }
}
