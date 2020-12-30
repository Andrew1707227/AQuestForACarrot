using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointDetector : MonoBehaviour {

    public bool activated;
    public GameObject Player;
    
    void Start() {
        activated = false;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && !activated) {
            activated = true;
            Player.GetComponent<PlayerHealthScript>().Checkpoint = gameObject;
        }
    }
}
