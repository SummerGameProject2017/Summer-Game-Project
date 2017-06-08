using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour {

    float alpha;

    // Use this for initialization
    void Start () {
        alpha = 0;
        GetComponent<GUITexture>().color = new Color(1, 1, 1, alpha);
    }
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject.activeSelf)
        {
            alpha = Mathf.Lerp(alpha, 1, Time.deltaTime);

            GetComponent<GUITexture>().color = new Color(1, 1, 1, alpha);
        } 
	}
   


  
}
