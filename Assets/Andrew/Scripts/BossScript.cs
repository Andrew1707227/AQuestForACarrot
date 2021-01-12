using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

    public GameObject carrot;
    public GameObject Boss;
    private Vector3 bossPos;
    private Animator bossAnim;

    private bool isDead;
    private float timer;

    void Start() {
        carrot.SetActive(false);
        Boss.SetActive(false);
        isDead = false;
        bossAnim = Boss.GetComponent<Animator>();
        timer = 0;
    }

    // Update is called once per frame
    void Update() {
        if (bossAnim.GetCurrentAnimatorStateInfo(0).IsName("Boss_Die")) {
            isDead = true;
        } else {
            bossPos = Boss.transform.position;
        }
        if (isDead) timer += Time.deltaTime;
        if (timer > 2 && timer < 2.1) {
            carrot.SetActive(true);
            carrot.transform.position = bossPos + new Vector3(-2,2,0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            Boss.SetActive(true);
        }
    }
}
