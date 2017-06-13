using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIN : MonoBehaviour {

    Image image;
    bool fadeIn = true;
    GameOver gameOverScript;

    // Use this for initialization
    void Start ()
    {
        gameOverScript = GameObject.Find("GAMEOVER").GetComponent<GameOver>();
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
            c.a += Time.deltaTime;
            image.color = c;
        }
        else
        {
            Color c = image.color;
            c.a -= Time.deltaTime ;
            image.color = c;
        }

        if (gameOverScript.continueButtonPushed == true)
        {
            fadeIn = false;
            StartCoroutine(gameOverScript.UnloadLevel());        }
    }
}
