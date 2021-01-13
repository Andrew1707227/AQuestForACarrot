using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

    public GameObject carrot;
    public GameObject Boss;
    public GameObject BossHealthBar;

    private GameObject BossClone;
    private bool debounce;
    private Vector3 bossPos;

    private bool isDead;
    private float timer;

    void Start() {
        carrot.SetActive(false);
        Boss.SetActive(false);
        isDead = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update() {
        if (BossClone == null) return;
        if (BossClone.GetComponent<BossBunnyHealth>().getLives() <= 0) {
            isDead = true;
        } else {
            bossPos = BossClone.transform.position;
        }
        if (isDead) timer += Time.deltaTime;
        if (timer > 2 && timer < 2.1) {
            carrot.SetActive(true);
            carrot.transform.position = bossPos + new Vector3(-2,2,0);
        }
        if (PlayerHealthScript.GetLives() <= 0) {
            //reset the boss trigger
            BossHealthBar.SetActive(false);
            if (BossClone != null) Destroy(BossClone);
            debounce = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && !debounce) {
            debounce = true;
            BossClone = Instantiate(Boss);
            BossClone.name = "BossBun";
            BossClone.SetActive(true);
            BossHealthBar.SetActive(true);
        }
    }
}
