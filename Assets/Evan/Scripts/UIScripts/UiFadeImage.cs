using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFadeImage : MonoBehaviour
{
    //Component Reference
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if cutscene has started
        if (StartGame.startCutscene)
        {
            //Starts coroutine
            StartCoroutine(fadeOutCor());
        }
    }

    IEnumerator fadeOutCor()
    {
        //Checks if completely transparent
        if (sr.color != new Color(1.0f, 1.0f, 1.0f, 0))
        {
            //Decreases alpha by 0.05
            sr.color = sr.color - new Color(0f, 0f, 0f, 0.05f);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
