using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStillStart : MonoBehaviour
{
    //Holds if the boss has stopped yelling
    public static bool isStarted = false;
    //Holds the time to wait at startup
    [SerializeField]
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        isStarted = false;  
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime > 0)
        {
            startTime -= Time.deltaTime;
        }
        else
        {
            isStarted = true;
        }
    }
}
