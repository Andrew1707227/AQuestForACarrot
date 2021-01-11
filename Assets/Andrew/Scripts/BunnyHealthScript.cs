using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyHealthScript : MonoBehaviour {

    public float maxLives;
    private float currLives;

    private bool debounce;
    private float coolDown;

    private hitEffect hitEffect;
    private Animator anim;

    private float aniTimer;
    private bool isDead;
    private Vector3 tempPos;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        hitEffect = GetComponent<hitEffect>();
        currLives = maxLives;
        aniTimer = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Snowball" && !debounce) {
            debounce = true; //keep it from hitting twice
            if (currLives - 1 <= 0) {
                isDead = true;
                GetComponent<CoolWanderingGoomba>().enabled = false;
                GetComponent<BunnyAnimator>().enabled = false;
                GetComponent<CapsuleCollider2D>().enabled = false;
                anim.enabled = true;
                anim.Play("BunnyDead");
                tempPos = transform.position;
            }
            hitEffect.hitEffectStart();
            currLives--;
        }
    }

    void Update() {
        if (debounce) coolDown += Time.deltaTime;
        if (coolDown > .05) {
            debounce = false;
            coolDown = 0;
        }
        if (isDead) {
            aniTimer += Time.deltaTime;
            transform.position = tempPos;
        }
        if (aniTimer > 1) Destroy(gameObject);
    }
    public float getLives() {
        return currLives;
    }
}
