using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBunnyHealth : MonoBehaviour
{
    //Holds max lives
    public float maxLives;
    //Holds current lives
    private float currLives;

    //Holds if the Boss can be hit
    private bool BunInvuln;
    //Holds hit cooldown
    private float coolDown;

    //Component References
    private hitEffect hitEffect;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        hitEffect = GetComponent<hitEffect>();
        currLives = maxLives;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Snowball" && !BunInvuln)
        {
            BunInvuln = true; //keep it from hitting twice
            if (currLives - 1 <= 0)
            {
                Destroy(gameObject);
            }

            hitEffect.hitEffectStart();

            currLives--;
        }
    }

    void Update()
    {
        //if debouce is active
        if (BunInvuln)
        {
            coolDown += Time.deltaTime;
        }

        if (coolDown > .05)
        {
            BunInvuln = false;
            coolDown = 0;
        }
    }

    //Gets current lives
    public float getLives()
    {
        return currLives;
    }
}
