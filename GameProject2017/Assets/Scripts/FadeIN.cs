using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIN : MonoBehaviour {

    Image image;
    bool fadeIn = true;
    bool fadeOut = false;
    GameOverButtons gameOverScript;

    // Use this for initialization
    void Start ()
    {

        gameOverScript = GameObject.Find("GAMEOVER").GetComponent<GameOverButtons>();
        image = GetComponent<Image>();
        Color c = image.color;
        c.a = 0;
        image.color = c;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (fadeIn == true)
        {
            
            Color c = image.color;
            if (c.a < 1)
           {
                c.a += Time.deltaTime;
                image.color = c;
            }
            else
            {
                fadeIn = false;
            }
        }
        if (fadeOut == true)
        {

            Color c = image.color;
            if (c.a > 0)
            {
                c.a -= Time.deltaTime;
                image.color = c;
            
            gameOverScript.fadeOut = false;
            }
            else
            {
                fadeOut = false;
            }
        }

        if (gameOverScript.continueButtonPushed == true || gameOverScript.fadeOut == true)
        {
            fadeIn = false;
            fadeOut = true;
        }
    }

}
