using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurserReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Sets cursor to default
        Cursor.SetCursor(null, new Vector2(), CursorMode.Auto);
    }
}
