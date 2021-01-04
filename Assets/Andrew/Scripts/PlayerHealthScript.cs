using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour {

    public float lives;
    private float currLives;
    public float OffMap;

    public GameObject Cam;
    private CameraScript CamShake;
    private hitEffect hitEffect;

    public GameObject DamageSFX;
    private AudioSource Asource;
    private Rigidbody2D rb2;

    public float cooldown;
    private float timeRemaining;
    public GameObject Checkpoint;

    // Start is called before the first frame update
    void Start() {
        CamShake = Cam.GetComponent<CameraScript>();
        hitEffect = GetComponent<hitEffect>();

        Asource = DamageSFX.GetComponent<AudioSource>();
        rb2 = GetComponent<Rigidbody2D>();

        timeRemaining = 0;
        currLives = lives;
    }

    // Update is called once per frame
    void Update() {
        if (timeRemaining >= 0) timeRemaining -= Time.deltaTime;
        if (transform.position.y < OffMap || currLives <= 0) {
            Respawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy" && timeRemaining <= 0 && !hitEffect.invuln) {
            currLives--;
            timeRemaining = cooldown;
            Asource.Play();

            CamShake.StartShake();
            hitEffect.hitEffectStart();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name == "DeathAreas") {
            Respawn();
        }
    }

    private void Respawn() {
        //TODO: Play death animation
        transform.position = Checkpoint.transform.position;
        rb2.velocity = new Vector2();
        currLives = lives;
    }

    public float GetLives() {
        return currLives;
    }
}
