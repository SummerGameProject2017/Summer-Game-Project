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
    AsyncOperation async;
    private void Start()
    {
        background.SetActive(false);
    }


    // Update is called once per frame
    void Update () {
        SceneManager.sceneLoaded += LevelWasLoaded;//in Unity 5 have to add function call to Scene manager. scene loaded 
    }

    IEnumerator DisplayLoadingScreen(string sceneName)
    {
        loadLevel = false;
        background.SetActive(true);


        async = SceneManager.LoadSceneAsync(sceneName);
        

        yield return null;
       
         
           
        


    }


    void LevelWasLoaded(Scene scene, LoadSceneMode mode)
    {
        if (loadGame == true)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().newGame = false;
            GameObject.Find("PlayerCamera").GetComponent<CameraView>().newGame = false;
            SaveLoad.Load();
        }
    }

    public void StartButtonFunction()
    {
        loadSceneName = "Junkyard_Level_VR";
        StartCoroutine(DisplayLoadingScreen(loadSceneName));
        SaveLoad.Save();
    }

    public void LoadButtonFunction()
    {
        loadGame = true;
        loadSceneName = "Junkyard_Level_VR";
        StartCoroutine(DisplayLoadingScreen(loadSceneName));
        Time.timeScale = 1;
        
        
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
