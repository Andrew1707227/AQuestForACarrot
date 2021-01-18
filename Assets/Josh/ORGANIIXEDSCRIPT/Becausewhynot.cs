using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Becausewhynot : MonoBehaviour
{
    void MovePlayer()
    {
        gameObject.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }
    public void KillPLayer()
    {
        Destroy(this.gameObject);
    }
}
