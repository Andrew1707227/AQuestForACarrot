using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBarManager : MonoBehaviour
{
    //Holds color gradient for health bar
    public Gradient gradient;

    //Component References
    private Slider s;
    private BossBunnyHealth bbh;
    Image i;

    // Start is called before the first frame update
    void Start()
    {
        //Get component References 
        s = GetComponent<Slider>();
        bbh = GameObject.Find("BossBun").GetComponent<BossBunnyHealth>();
        i = GameObject.Find("Bar").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //if current bar value is bigger than health precentage
        if (s.value > (bbh.getLives() / bbh.getMaxLives()))
        {
            //Increment health down
            s.value -= 0.005f;
        //CHange color based on slide location
            i.color = gradient.Evaluate(s.normalizedValue);
        }
    }
}
