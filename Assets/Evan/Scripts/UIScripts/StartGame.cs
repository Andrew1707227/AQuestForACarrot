using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StartGame : MonoBehaviour
{
    GameObject firstFrame;

    public bool startCutscene = false;

    //Component Reference
    private VideoPlayer vp;

    private void Start()
    {
        GameObject videoPlayer = GameObject.Find("Video Player");
        vp = videoPlayer.GetComponent<VideoPlayer>();

        firstFrame = GameObject.Find("FirstFrame");
    }

    
    private void Update()
    {
        if (vp.isPlaying)
        {
            // working here
            // solution: make first frame of video image over screen and when playing remove that and show video player instead
            if (vp.isPlaying)
            {
                //firstFrame.active = false;
            }
        }
    }

    public void startClick()
    {
        startCutscene = true;
        StartCoroutine(startVideoCor());
    }

    IEnumerator startVideoCor()
    {
        yield return new WaitForSeconds(1f);
        vp.Play();
        yield return new WaitForSeconds(0.6f);
        firstFrame.active = false;
    }
}
