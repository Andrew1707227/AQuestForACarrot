using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiFadeText : MonoBehaviour
{
    private bool fadeStart;

    //Component References
    Text t;
    StartGame startGame;

    // Start is called before the first frame update
    void Start()
    {
        t = gameObject.GetComponent<Text>();

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
        if (t.color != new Color(1.0f, 1.0f, 1.0f, 0))
        {
            t.color = t.color - new Color(0f, 0f, 0f, 0.05f);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
