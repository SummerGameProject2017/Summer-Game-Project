using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIN : MonoBehaviour {

    Image image;

    

    // Use this for initialization
    void Start ()
    {
        image = GetComponent<Image>();
        Color c = image.color;
        c.a = 0;
        image.color = c;

        GameObject varGameObject = GameObject.FindWithTag("Player");


        varGameObject.GetComponent<PlayerController>().enabled = false;

    }
	
	// Update is called once per frame
	void Update ()
    {
        Color c = image.color;
        c.a += Time.deltaTime;
        
        image.color = c;

    }
}
