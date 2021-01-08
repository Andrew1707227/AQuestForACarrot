using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    private int currentScreen = 0;
    public GameObject[] creditScreens = new GameObject[6];

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i <= 5; i++)
        {
            if (i == currentScreen)
            {
                creditScreens[i].SetActive(true);
            }
            else
            {
                creditScreens[i].SetActive(false);
            }
        }
    }

    public void EvanCall()
    {
        currentScreen = 0;
    }

    public void AndrewCall()
    {
        currentScreen = 1;
    }

    public void JoshCall()
    {
        currentScreen = 2;
    }

    public void JaydaCall()
    {
        currentScreen = 3;
    }

    public void MasonCall()
    {
        currentScreen = 4;
    }

    public void JadeCall()
    {
        currentScreen = 5;
    }
}
