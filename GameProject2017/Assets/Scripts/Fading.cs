using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fading : MonoBehaviour {

    public float alpha;
    public bool fadeIn = false;
    Image image;
    GameObject loadingCamera;

    // Use this for initialization
    void Start () {
        fadeIn = true;
        image = GetComponent<Image>();
        Color c = image.color;
        c.a = 0;
        image.color = c;
        loadingCamera = GameObject.Find("LoadingCamera");
    }
	
	// Update is called once per frame
	void Update () {
		if (fadeIn == true)
        {
            Color c = image.color;
            if (c.a < 1)
            {
                c.a += Time.deltaTime;
                image.color = c;
            }
        }

        if (ChangeScene.doneLoading == true)
        {
            loadingCamera.transform.position = GameObject.Find("PlayerCamera").transform.position;
            fadeIn = false;
            Color c = image.color;
            if (c.a > 0)
            {
                c.a -= Time.deltaTime;
                image.color = c;
            }
        }
	}
   


  
}
