using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
    public GameObject background;
    public bool loadLevel = false;
    private int loadProgress = 0;
    bool loadGame = false;
    string loadSceneName;

    private void Start()
    {
        background.SetActive(false);
    }


    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator DisplayLoadingScreen(string sceneName)
    {
        loadLevel = false;
        background.SetActive(true);
 
       

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        while (!async.isDone)
        {
            loadProgress = (int)(async.progress * 100);


            yield return null;
        }
       
        if (async.isDone && loadGame == true)
        {
            SaveLoad.Load();
        }


    }



    public void StartButtonFunction()
    {
        loadSceneName = "Junkyard_Level_VR";
        StartCoroutine(DisplayLoadingScreen(loadSceneName));
    }

    public void LoadButtonFunction()
    {
        loadSceneName = "Junkyard_Level_VR";
        StartCoroutine(DisplayLoadingScreen(loadSceneName));
        Time.timeScale = 1;
        loadGame = true;
         
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    public void ReturnToMainMenu()
    {
        loadSceneName = "Main_Menu";
        StartCoroutine(DisplayLoadingScreen(loadSceneName));
        Time.timeScale = 1;
    }

}
