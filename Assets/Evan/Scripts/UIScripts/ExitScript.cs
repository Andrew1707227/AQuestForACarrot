using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public void quitTheGame()
    {
        //Quits game
        Application.Quit();
        Debug.Log("Would of quit if built");
    }
}
