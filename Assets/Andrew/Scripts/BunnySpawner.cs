using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnySpawner : MonoBehaviour {
    public GameObject BunnyContainer;
    private GameObject BunnyContainerClone;

    private bool isDead;
    private float timer;

    // Start is called before the first frame update
    void Start() {
        BunnyContainer.SetActive(false);
        SpawnBunnies();
    }

    // Update is called once per frame
    void Update() {
        if (PlayerHealthScript.GetLives() <= 0) isDead = true;
        if (isDead) timer += Time.deltaTime;
        if (timer > 3) SpawnBunnies();
    }
    private void SpawnBunnies() {
        isDead = false;
        timer = 0;
        Destroy(GameObject.Find("BunnyContainerClone"));
        BunnyContainerClone = Instantiate(BunnyContainer);
        BunnyContainerClone.name = "BunnyContainerClone";
        BunnyContainerClone.SetActive(true);
    }
}
