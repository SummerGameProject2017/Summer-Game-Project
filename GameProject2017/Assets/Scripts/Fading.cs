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
    Color c;

    // Use this for initialization
    void Start () {
        fadeIn = true;
        image = GetComponent<Image>();
        c = image.color;
        c.a = 0;
        image.color = c;
        loadingCamera = GameObject.Find("LoadingCamera");
    }
	
	// Update is called once per frame
	void Update () {
		if (fadeIn == true)
        {
            if (c.a < 1)
            {
                c.a += Time.deltaTime;
                image.color = c;
            }
        }

        if (ChangeScene.doneLoading == true)
        {
            if (SceneManager.GetSceneByName("Junkyard_Level_VR").isLoaded)
            {
                loadingCamera.transform.position = GameObject.Find("PlayerCamera").transform.position;
                loadingCamera.transform.rotation = GameObject.Find("PlayerCamera").transform.rotation;
            }
            if (SceneManager.GetSceneByName("Forest_Level_VR").isLoaded)
            {
                loadingCamera.transform.position = GameObject.Find("PlayerCamera").transform.position;
                loadingCamera.transform.rotation = GameObject.Find("PlayerCamera").transform.rotation;
            }
            fadeIn = false;
            if (c.a > 0)
            {
                c.a -= Time.deltaTime;
                image.color = c;
            }

        }
    }
   


  
}
