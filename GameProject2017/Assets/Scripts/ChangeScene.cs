using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
    public GameObject background;
    public GameObject text;
    public GameObject progressBar;
    public bool loadLevel = false;
    private int loadProgress = 0;



    // Use this for initialization
    void Start () {
        background.SetActive(false);
        text.SetActive(false);
        progressBar.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if (loadLevel == true)
        {
            StartCoroutine(DisplayLoadingScreen());
        }
	}

    IEnumerator DisplayLoadingScreen()
    {
        GameObject.Find("Canvas").SetActive(false);
        loadLevel = false;
        background.SetActive(true);
        text.SetActive(true);
        progressBar.SetActive(true);

        progressBar.transform.localScale = new Vector3(loadProgress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);
        text.GetComponent<GUIText>().text = "Loading Progress " + loadProgress + " %";

        AsyncOperation async = SceneManager.LoadSceneAsync("Junkyard_Level_VR");

        while (!async.isDone)
        {
            loadProgress = (int)(async.progress * 100);
            text.GetComponent<GUIText>().text = "Loading Progress " + loadProgress + " %";
            progressBar.transform.localScale = new Vector3(async.progress, progressBar.transform.localScale.y, progressBar.transform.localScale.z);


            yield return null;
        }


    }
}
