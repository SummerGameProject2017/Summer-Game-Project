using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDeadRusty : MonoBehaviour {

    FadeIN fadeScript;
    GameObject deadRusty;
	// Use this for initialization
	void Start () {
        deadRusty = GameObject.Find("DeadRusty");
        deadRusty.SetActive(false);
        fadeScript = GameObject.Find("Image").GetComponent<FadeIN>();
	}
	
	// Update is called once per frame
	void Update () {
		if (fadeScript.showRusty == true)
        {
            deadRusty.SetActive(true);
        }
        else
        {
            deadRusty.SetActive(false);
        }
	}
}
