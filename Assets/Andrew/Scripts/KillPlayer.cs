using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour {
    public GameObject spawnPoint;
    public GameObject mainPlayer;
    public float offMap;
    private Rigidbody2D rb2;

   void Update() {
       if (mainPlayer.transform.position.y < offMap) {
            mainPlayer.transform.localPosition = spawnPoint.transform.localPosition;
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            rb2 = mainPlayer.GetComponent<Rigidbody2D>();
            rb2.velocity = new Vector2(0, 0);
            mainPlayer.transform.localPosition = spawnPoint.transform.localPosition;
        }
    }
}