using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiFadeButtons : MonoBehaviour
{
    private bool fadeStart;

    //Component References
    Image i;
    StartGame startGame;

    // Start is called before the first frame update
    void Start()
    {
        i = gameObject.GetComponent<Image>();

        GameObject startbutton = GameObject.Find("StartButton");
        startGame = startbutton.GetComponent<StartGame>();
        fadeStart = startGame.startCutscene;
    }

    // Update is called once per frame
    void Update()
    {
        fadeStart = startGame.startCutscene;

        if (fadeStart)
        {
            StartCoroutine(fadeOutCor());
        }
    }

    IEnumerator fadeOutCor()
    {
        if (i.color != new Color(1.0f, 1.0f, 1.0f, 0))
        {
            i.color = i.color - new Color(0f, 0f, 0f, 0.05f);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
