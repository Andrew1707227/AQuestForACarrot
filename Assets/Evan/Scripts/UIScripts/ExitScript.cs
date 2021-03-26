using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public void quitTheGame()
    {
        //Quits game
        if (Application.platform == RuntimePlatform.WebGLPlayer) {
            Screen.fullScreen = false;
        } else {
            Application.Quit();
            Debug.Log("Would of quit if built");
        }
    }
}
