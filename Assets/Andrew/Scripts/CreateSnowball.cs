using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSnowball : MonoBehaviour {

    public GameObject SnowballTemplate;

    public float CoolDown;
    private float TimeRemaining;

    private AudioSource ASource;
    public Texture2D crosshair;
    public Vector2 crosshairPos;
   
    // Start is called before the first frame update
    void Start() {
        SnowballTemplate.transform.position = new Vector3(9999, 9999); //im not a bad coder you are
        SnowballTemplate.GetComponent<Rigidbody2D>().gravityScale = 0;
        ASource = GetComponent<AudioSource>();
        TimeRemaining = CoolDown;
        Cursor.SetCursor(crosshair, crosshairPos, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update() {
        if (TimeRemaining <= CoolDown) TimeRemaining += Time.deltaTime;
        
        if (Input.GetButton("Fire1") && TimeRemaining >= CoolDown) {
            TimeRemaining = 0;
            ASource.pitch = Random.value / 5 + .9f;
            ASource.Play();
            GameObject Snowball = Instantiate(SnowballTemplate);
            Snowball.name = "SnowballClone";
        }
    }
}
