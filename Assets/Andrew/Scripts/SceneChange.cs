﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {
    public string GoTo;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            SceneManager.LoadScene(GoTo);
        }
    }
}