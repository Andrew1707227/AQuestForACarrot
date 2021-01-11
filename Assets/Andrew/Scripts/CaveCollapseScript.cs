using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveCollapseScript : MonoBehaviour {

    public GameObject Music;
    public GameObject BossTheme;
    public GameObject Player;
    public GameObject cam;
    public Sprite defaultPlayerSprite;

    private bool debounce = false;
    private float timer;
    private Vector3 tempPos;

    private AudioSource Asource;
    private ParticleSystem ps;
    private PolygonCollider2D collision;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start() {
        Asource = GetComponent<AudioSource>();
        ps = GetComponent<ParticleSystem>();
        collision = GetComponent<PolygonCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()  {
        if (debounce) timer += Time.deltaTime;
        if (timer > 5) {
            Player.GetComponent<PlayerControl>().enabled = true;
            Player.GetComponent<PlayerHealthScript>().enabled = true;
            //Player.GetComponent<Animator>().enabled = true;
        } else if (timer > 2) {
            collision.enabled = false;
            ps.Play();
            sr.enabled = false;
            Player.transform.position = new Vector2(tempPos.x, Player.transform.position.y);
        } else if (timer > 0) {
            Player.transform.position = new Vector2(tempPos.x, Player.transform.position.y);
            cam.GetComponent<CameraScript>().StartShake();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && !debounce) {
            debounce = true;
            Player.GetComponent<PlayerControl>().enabled = false;
            Player.GetComponent<PlayerHealthScript>().enabled = false;
            //Player.GetComponent<Animator>().enabled = false;
            Player.GetComponent<Animator>().SetBool("aniMoving",false);
            //Player.GetComponent<SpriteRenderer>().sprite = defaultPlayerSprite;
            Music.GetComponent<AudioSource>().enabled = false;
            Asource.Play();
            BossTheme.GetComponent<AudioSource>().PlayDelayed(2);
            tempPos = Player.transform.position;
        }
    }
}
