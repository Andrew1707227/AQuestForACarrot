using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveCollapseScript : MonoBehaviour {

    public GameObject Music;
    private bool debounce = false;
    private AudioSource Asource;
    // Start is called before the first frame update
    void Start() {
        Asource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()  {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player" && !debounce) {
            debounce = true;
            Music.GetComponent<AudioSource>().enabled = false;
            Asource.Play();
        }
    }
}
