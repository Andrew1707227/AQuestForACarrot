using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFadeImage : MonoBehaviour
{
    private bool fadeStart;

    //Component References
    SpriteRenderer sr;
    StartGame startGame;

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();

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
        if (sr.color != new Color(1.0f, 1.0f, 1.0f, 0))
        {
            sr.color = sr.color - new Color(0f, 0f, 0f, 0.05f);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
