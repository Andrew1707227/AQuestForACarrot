using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBunnyActivator : MonoBehaviour {
    // Start is called before the first frame update
    public GameObject Checkpoint;
    void Start() {
        GetComponent<SNOWANDBALLS>().enabled = false; 
    }

    // Update is called once per frame
    void Update() {
        if (Checkpoint.GetComponent<CheckpointDetector>().activated) {
            GetComponent<SNOWANDBALLS>().enabled = GetComponents<CapsuleCollider2D>()[0].enabled;
        }
    }
}
