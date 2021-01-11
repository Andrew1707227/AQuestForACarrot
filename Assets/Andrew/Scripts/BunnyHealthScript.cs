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

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        hitEffect = GetComponent<hitEffect>();
        currLives = maxLives;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Snowball" && !debounce) {
            debounce = true; //keep it from hitting twice
            if (currLives - 1 <= 0) {
                Destroy(gameObject);
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
    }
    public float getLives() {
        return currLives;
    }
}
