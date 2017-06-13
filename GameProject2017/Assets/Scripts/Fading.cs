using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour {

    float alpha;
    bool fadeIn = false;
    public bool fadeOut = false;

    // Use this for initialization
    void Start () {
        fadeIn = true;
        alpha = 0;
        GetComponent<GUITexture>().color = new Color(1, 1, 1, alpha);
    }
	
	// Update is called once per frame
	void Update () {
		if (fadeIn == true)
        {
            alpha = Mathf.Lerp(alpha, 1, Time.deltaTime);

            GetComponent<GUITexture>().color = new Color(1, 1, 1, alpha);
        } 
	}
   


  
}
