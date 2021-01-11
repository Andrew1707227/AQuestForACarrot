using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class WinSceneChange : MonoBehaviour {
    private VideoPlayer vp;
    private bool vidStarted;
    // Start is called before the first frame update
    void Start() {
        vp = GetComponent<VideoPlayer>();
        vidStarted = false;
        vp.Prepare();
    }

    // Update is called once per frame
    void Update() {
       if (vp.isPlaying) vidStarted = true;
       if(!vp.isPlaying && vidStarted) {
            SceneManager.LoadScene("CreditScene");  
        }
    }
}
