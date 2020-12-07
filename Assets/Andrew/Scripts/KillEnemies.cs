using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemies : MonoBehaviour {
    //Note: this is designed to be used with the snowball
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Enemy") {
            GameObject currEnemy = collision.gameObject;
            //kill the enemy
        }
        Destroy(gameObject);
    }
}
