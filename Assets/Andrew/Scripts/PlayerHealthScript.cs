using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour {

    public float lives;
    private static float currLives;
    public float OffMap;

    public GameObject Cam;
    private CameraScript CamShake;
    private hitEffect hitEffect;

    public GameObject DamageSFX;
    private AudioSource Asource;
    public GameObject DiedSFX;

    private Rigidbody2D rb2;
    private Animator anim;

    public float cooldown;
    private float timeRemaining;
    public GameObject Checkpoint;

    private float aniDeadCoolDown;
    private bool isDead;
    private Vector3 tempPos; // to lock the player in place when they die

    // Start is called before the first frame update
    void Start() {
        CamShake = Cam.GetComponent<CameraScript>();
        hitEffect = GetComponent<hitEffect>();

        Asource = DamageSFX.GetComponent<AudioSource>();
        rb2 = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        timeRemaining = 0;
        aniDeadCoolDown = 0;
        isDead = false;
        currLives = lives;
    }

    // Update is called once per frame
    void Update() {
        if (timeRemaining >= 0) timeRemaining -= Time.deltaTime;
        if (aniDeadCoolDown >= 0) {
            aniDeadCoolDown -= Time.deltaTime;
        } else {
            anim.ResetTrigger("aniDead");  
        }
        if (transform.position.y < OffMap || currLives <= 0) {
            killPlayer();
        }
        if (isDead) {
            GetComponent<PlayerControl>().enabled = false;
            GetComponent<CreateSnowball>().enabled = false;
            transform.position = tempPos;
            if (aniDeadCoolDown <= 0) Respawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy" && timeRemaining <= 0 && !hitEffect.invuln && !isDead) {
            currLives--;
            timeRemaining = cooldown;
            Asource.Play();

            CamShake.StartShake();
            hitEffect.hitEffectStart();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name == "DeathAreas") {
            currLives = 0;
        }
    }

    private void killPlayer() {
        if (!isDead) {
            anim.SetTrigger("aniDead");
            DiedSFX.GetComponent<AudioSource>().Play();
            aniDeadCoolDown = 3;
            isDead = true;
            tempPos = transform.position;
        }
    }

    private void Respawn() {
        isDead = false;
        transform.position = Checkpoint.transform.position;
        rb2.velocity = new Vector2();
        currLives = lives;
        anim.Play("Player_idle");
        GetComponent<PlayerControl>().enabled = true;
        GetComponent<CreateSnowball>().enabled = true;
    }

    public static float GetLives() {
        return currLives;
    }
}
