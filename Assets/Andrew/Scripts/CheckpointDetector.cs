using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointDetector : MonoBehaviour {

    public bool activated;
    public GameObject Player;
    private Animator anim;
    
    void Start() {
        activated = false;
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && !activated) {
            anim.enabled = true;
            activated = true;
            Player.GetComponent<PlayerHealthScript>().Checkpoint = gameObject;
            GetComponent<AudioSource>().Play();
        }
    }
}
