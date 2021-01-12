using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSnowBallThrow : MonoBehaviour
{
    //Holds throw snowball template to spawn
    [SerializeField]
    private GameObject throwSnowballTemplate;
    //Holds drop snowball template to spawn
    [SerializeField]
    private GameObject dropSnowballTemplate;

    //Holds time in between throws
    [SerializeField]
    private float throwCoolDownTime;
    //Holds time in between Drops
    [SerializeField]
    private float dropCoolDownTime;
    //Holds current CoolDown Time
    private float currentThrowCoolTime;
    //Holds time in between Drops
    private float currentDropCoolTime;

    //Times out the cooldown for throw
    private float throwCoolDownTimer;
    //Times out the cooldown for drop
    private float dropCoolDownTimer;

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
        currentThrowCoolTime = throwCoolDownTime;

        //Sets up starting cool down time
        currentDropCoolTime = dropCoolDownTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Manages throw timing
        if (throwCoolDownTimer >= currentThrowCoolTime && !PauseMenu.gameIsPaused)
        {

            //Resets timer
            throwCoolDownTimer = 0;

            //Resets throwTriggered
            throwTriggered = false;

            //Adds a random valuse to the cooldown
            currentThrowCoolTime += Random.Range(-0.5f, 0.5f);

            //Sets the cooldown back if it gets to big or small
            if (currentThrowCoolTime > throwCoolDownTime + 1.5f)
            {
                currentThrowCoolTime = throwCoolDownTime + 1.5f;
            }
            else if (currentThrowCoolTime < throwCoolDownTime - 1.5f)
            {
                currentThrowCoolTime = throwCoolDownTime - 1.5f;
            }

            //Spawn snowball and changes its name
            GameObject throwSnowball = Instantiate(throwSnowballTemplate);
            throwSnowball.name = "BossSnowballCloneThrow";
        }
        else if (throwCoolDownTimer >= currentThrowCoolTime - 0.2 && !throwTriggered && !PauseMenu.gameIsPaused)
        {
            //Starts throw animation
            ani.SetTrigger("AniBossThrow");

            //Activateds throwTriggered
            throwTriggered = true;

            //Incremenets timer
            throwCoolDownTimer += Time.deltaTime;
        }
        else
        {
            //Incremenets timer
            throwCoolDownTimer += Time.deltaTime;
        }

        //Manages drop timing
        if (dropCoolDownTimer >= currentDropCoolTime && !PauseMenu.gameIsPaused)
        {

            //Resets timer
            dropCoolDownTimer = 0;

            //Adds a random valuse to the cooldown
            currentDropCoolTime += Random.Range(-0.5f, 0.5f);

            //Sets the cooldown back if it gets to big or small
            if (currentDropCoolTime > dropCoolDownTime + 1.5f)
            {
                currentDropCoolTime = dropCoolDownTime + 1.5f;
            }
            else if (currentDropCoolTime < dropCoolDownTime - 1.5f)
            {
                currentDropCoolTime = dropCoolDownTime - 1.5f;
            }

            //Spawn snowball and changes its name
            GameObject dropSnowball = Instantiate(dropSnowballTemplate);
            dropSnowball.name = "BossSnowballCloneDrop";
        }
        else
        {
            //Incremenets timer
            dropCoolDownTimer += Time.deltaTime;
        }
    }
}
