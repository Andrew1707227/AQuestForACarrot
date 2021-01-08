using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    //Holds current health
    private int currentHealth;
    //Holds max health
    private int maxHealth;

    //Holds refrences to all the heart images
    public Image[] hearts;
    //Holds full heart sprite
    public Sprite fullHeart;
    //Holds emtpy heart sprite
    public Sprite emptyHeart;

    private PlayerHealthScript pHS;

    // Start is called before the first frame update
    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        pHS = player.GetComponent<PlayerHealthScript>();
        maxHealth = (int)pHS.lives;

        currentHealth = (int)PlayerHealthScript.GetLives(); 
        //maxHealth = PlayerHealthScript.G
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = (int)PlayerHealthScript.GetLives();
        maxHealth = (int)pHS.lives;

        for (int i = 0; i < hearts.Length; i++)
        {

            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
