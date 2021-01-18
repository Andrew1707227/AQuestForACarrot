using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNOWANDBALLS : MonoBehaviour
{
    public GameObject SnowballTemplate;
    public Sprite OongaBoonga;
    public float CoolDown;
    private float Timer;
    
    private AudioSource ASource;
    private SpriteRenderer sr;
    private Animator ani;

   

    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;
        SnowballTemplate.transform.position = new Vector3(9999, 9999); //im not a bad coder you are
        SnowballTemplate.GetComponent<Rigidbody2D>().gravityScale = 0;

        ASource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        /* if (TimeRemaining <= CoolDown) TimeRemaining += Time.deltaTime;
         Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         float dx = transform.position.x - mousePos.x;
         bool flip = sr.flipX;

         if ((dx > 0 && !flip || dx < 0 && flip) && TimeRemaining >= CoolDown && !PauseMenu.gameIsPaused)
         {
             if (Input.GetButton("Fire1") && TimeRemaining >= CoolDown)
             {
                 TimeRemaining = 0;
                 ASource.pitch = Random.value / 5 + .9f;
                 ASource.Play();
                 ani.SetTrigger("aniThrow");
                 GameObject Snowball = Instantiate(SnowballTemplate);
                 Snowball.name = "SnowballClone";
             }
         }*/
        if (CoolDown <= 0)
        {
                        ani.enabled = true;
            CoolDown = 1.5f;
        }
        CoolDown -= Time.deltaTime;
        if (!ani.enabled) sr.sprite = OongaBoonga;
        if (ani.enabled) Timer += Time.deltaTime;
        if (Timer > .75 && Timer < .77)
        {
            ASource.pitch = Random.value / 5 + .9f;
            ASource.Play();

            GameObject Snowball = Instantiate(SnowballTemplate);
            Snowball.name = "SnowballClone";
            
        }
        if (Timer > 1)
        {
            ani.enabled = false;
            Timer = 0;
        }
    }
}
