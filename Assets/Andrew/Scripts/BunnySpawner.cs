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
        isDead = PlayerHealthScript.GetLives() <= 0;
        if (isDead) timer += Time.deltaTime;
        if (timer > 2 && !isDead) SpawnBunnies();
    }
    private void SpawnBunnies() {
        timer = 0;
        Destroy(GameObject.Find("BunnyContainerClone"));
        BunnyContainerClone = Instantiate(BunnyContainer);
        BunnyContainerClone.name = "BunnyContainerClone";
        BunnyContainerClone.SetActive(true);
    }
}
