using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour { 
    public GameObject player;
    public Vector3 offset;
    public bool testButton;

    public float CamShakeAmount;
    public float CamShakeDuration;
    private float ShakeRemaining;

    // Start is called before the first frame update
    void Start() {
        ShakeRemaining = 0;
    }

    // Update is called once per frame
    void LateUpdate() {
        transform.position = player.transform.position + offset;
        CameraShake();
        if (testButton) {
            StartShake();
        }
    }

    public void StartShake() {
        ShakeRemaining = CamShakeDuration;
    }

    private void CameraShake() {
        if (ShakeRemaining <= 0) return;
            testButton = false;
            // Adds a random Vector3 to the cam pos
            transform.position = player.transform.position + offset + Random.insideUnitSphere * CamShakeAmount;
            ShakeRemaining -= Time.deltaTime;
    }
}

