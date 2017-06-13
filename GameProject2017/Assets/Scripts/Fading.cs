using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fading : MonoBehaviour {

    public float alpha;
    public bool fadeIn = false;

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
            alpha = Mathf.Lerp(alpha, 1, Time.deltaTime * 2);

            GetComponent<GUITexture>().color = new Color(1, 1, 1, alpha);
        } 

        if (ChangeScene.doneLoading == true)
        {
            fadeIn = false;
            alpha = Mathf.Lerp(alpha, 0, Time.deltaTime * 2);

            GetComponent<GUITexture>().color = new Color(1, 1, 1, alpha);
            
        }
	}
   


  
}
