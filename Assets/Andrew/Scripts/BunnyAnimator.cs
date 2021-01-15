using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyAnimator : MonoBehaviour {

    private Animator anim;
    private SpriteRenderer sr;
    public Sprite defaultBunny;
    public GameObject checkpoint;
    private CoolWanderingGoomba goomba;
    private Vector3 prevTransform;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        goomba = GetComponent<CoolWanderingGoomba>();
        goomba.enabled = false;
    }

    // Update is called once per frame
    void Update()  {
        if (!goomba.enabled && checkpoint.GetComponent<CheckpointDetector>().activated) goomba.enabled = true;
        sr.flipX = transform.position.x - prevTransform.x > 0;
        anim.enabled = Mathf.Abs(transform.position.magnitude - prevTransform.magnitude) > .01;
        if (!anim.enabled) sr.sprite = defaultBunny;
        prevTransform = transform.position;
    }
}
