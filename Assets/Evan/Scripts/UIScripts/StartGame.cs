using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    //Holds reference to first frame image
    GameObject firstFrame;

    //Holds if cutscene has started
    public static bool startCutscene = false;

    //Component Reference
    private VideoPlayer vp;

    private void Start()
    {
        GameObject videoPlayer = GameObject.Find("Video Player");
        vp = videoPlayer.GetComponent<VideoPlayer>();

        firstFrame = GameObject.Find("FirstFrame");
    }

    //If button is clicked
    public void startClick()
    {
        //set StartCutscene to true
        startCutscene = true;
        //Start coroutine
        StartCoroutine(startVideoCor());
    }

    IEnumerator startVideoCor()
    {
        yield return new WaitForSeconds(1f);

        //Start videoplayer
        vp.Play();
        yield return new WaitForSeconds(0.6f);

        //Turns off first frame image
        firstFrame.SetActive(false);

        //Waits for video to end
        while (vp.isPlaying)
        {
            yield return new WaitForSeconds(0.1f);
        }

        //Loads Level Scene
        SceneManager.LoadScene("MainLevel");
    }
}
