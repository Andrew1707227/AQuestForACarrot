using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSnowBallThrow : MonoBehaviour
{
    //Holds snowball temeplate to spawn
    [SerializeField]
    private GameObject snowballTemplate;

    //Holds time in between throws
    [SerializeField]
    private float coolDownTime;
    //Holds current CoolDown Time
    private float currentCoolDownTime;

    //Times out the cooldown
    private float coolDownTimer;

    //Holds if throwAnimation has started
    private bool throwTriggered = false;

    //Component References
    private AudioSource aS;
    private SpriteRenderer sr;
    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        //Get component References
        aS = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();

        //Sets up starting cool down time
        currentCoolDownTime = coolDownTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDownTimer >= currentCoolDownTime && !PauseMenu.gameIsPaused)
        {

            //Resets timer
            coolDownTimer = 0;

            //Resets throwTriggered
            throwTriggered = false;

            //Adds a random valuse to the cooldown
            currentCoolDownTime += Random.Range(-0.5f, 0.5f);

            //Sets the cooldown back if it gets to big or small
            if (currentCoolDownTime > coolDownTime + 1.5f)
            {
                currentCoolDownTime = coolDownTime + 1.5f;
            }
            else if (currentCoolDownTime < coolDownTime - 1.5f)
            {
                currentCoolDownTime = coolDownTime - 1.5f;
            }

            //Spawn snowball and changes its name
            GameObject snowball = Instantiate(snowballTemplate);
            snowball.name = "BossSnowballClone";
        }
        else if(coolDownTimer >= currentCoolDownTime - 0.2 && !throwTriggered && !PauseMenu.gameIsPaused)
        {
            //Starts throw animation
            ani.SetTrigger("AniBossThrow");

            //Activateds throwTriggered
            throwTriggered = true;

            //Incremenets timer
            coolDownTimer += Time.deltaTime;
        }
        else
        {
            //Incremenets timer
            coolDownTimer += Time.deltaTime;
        }
    }
}
