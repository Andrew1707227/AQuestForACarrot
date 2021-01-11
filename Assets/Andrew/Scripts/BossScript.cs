using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

    public GameObject carrot;
    public GameObject Boss;
    private Vector3 bossPos;

    void Start() {
        carrot.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
       if (Boss == null) {
            carrot.SetActive(true);
            carrot.transform.position = bossPos + Vector3.up;
        } else {
            bossPos = Boss.transform.position;
        }
    }
}
