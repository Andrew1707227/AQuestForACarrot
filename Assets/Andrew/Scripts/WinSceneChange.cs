using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinSceneChange : MonoBehaviour {
    private VideoPlayer vp;
    private bool vidStarted;
    private float timer;

    public GameObject Text;

    // Start is called before the first frame update
    void Start() {
        vp = GetComponent<VideoPlayer>();
        vidStarted = false;
        Text.GetComponent<Text>().enabled = false;
        vp.Prepare();
        timer = 0;
    }

    // Update is called once per frame
    void Update() {
       if (vp.isPlaying) vidStarted = true;
       if(vidStarted && !vp.isPlaying) {
            Text.GetComponent<Text>().enabled = true;
            timer += Time.deltaTime;
        }
        if (timer > 2) SceneManager.LoadScene("CreditScene");
    }
}
