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
    PlayerController playerScript;
    CameraView cameraScript;
    GameObject gameoverScreen;
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
            playerScript.newGame = false;
            cameraScript.newGame = false;
            SaveLoad.Load();
        }
        else
        {
            SaveLoad.Save();
        }
    }

    public void StartButtonFunction()
    {
        loadSceneName = "Junkyard_Level_VR";
        StartCoroutine(DisplayLoadingScreen(loadSceneName));
    }

    public void LoadButtonFunction()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        cameraScript = GameObject.Find("PlayerCamera").GetComponent<CameraView>();
        playerScript.enabled = true;
        cameraScript.enabled = true;
        gameoverScreen = GameObject.Find("GAMEOVER");
        playerScript.newGame = false;
        cameraScript.newGame = false;
        SaveLoad.Load();

        loadGame = true;
        loadSceneName = "Junkyard_Level_VR";
    //    StartCoroutine(DisplayLoadingScreen(loadSceneName));
        Time.timeScale = 1;
        gameoverScreen.SetActive(false);
        
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
