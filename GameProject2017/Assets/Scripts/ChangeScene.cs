using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoSingleton<ChangeScene> {
    bool loadGame = false;
    bool saveGame = false;
    string loadSceneName;
    string addScreenName;
    AsyncOperation async;
    PlayerController playerScript;
    CameraView cameraScript;
    GameObject gameoverScreen;
    public static bool doneLoading = false;
    public static bool startGame = false;
    public override void OnStart()
    {
    }


    // Update is called once per frame
    public override void OnUpdate () {
        SceneManager.sceneLoaded += LevelWasLoaded;//in Unity 5 have to add function call to Scene manager. scene loaded  

    }

    IEnumerator DisplayLoadingScreen(string sceneName)
    {
        Scene startScene = SceneManager.GetActiveScene();
        Scene loadScene;
        Scene newScene;


        SceneManager.LoadSceneAsync(addScreenName, LoadSceneMode.Additive);
        loadScene = SceneManager.GetSceneByName(addScreenName);


        

        async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        newScene = SceneManager.GetSceneByName(sceneName);
       
        
         yield return new WaitForSecondsRealtime(1);

       
        if (async.progress > 0.9f)
        {
            doneLoading = true;
SceneManager.UnloadSceneAsync(startScene);

            
            yield return new WaitForSeconds(1);
            SceneManager.UnloadSceneAsync(loadScene);
            SceneManager.SetActiveScene(newScene);
            startGame = true;
            doneLoading = false;
        }

        yield return null;
       
    

    }


  




    void LevelWasLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Junkyard_Level_VR")
        {
           
            playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            cameraScript = GameObject.Find("PlayerCamera").GetComponent<CameraView>();
            if (loadGame == true)
            {

                loadGame = false;
                playerScript.newGame = false;
                cameraScript.newGame = false;
                SaveLoad.continueFromMain = true;
                SaveLoad.Load();
            }
            if (saveGame == true)
            {
                saveGame = false;
                playerScript.newGame = true;
                cameraScript.newGame = true;
            }
        }
    }

    public void StartButtonFunction()
    {
        
        saveGame = true;
        loadSceneName = "Junkyard_Level_VR";
        addScreenName = "LoadingLevel";
        StartCoroutine(DisplayLoadingScreen(loadSceneName));
    }

    public void LoadButtonFunction()
    {
        loadGame = true;
        loadSceneName = "Junkyard_Level_VR";
        addScreenName = "LoadingLevel";
        StartCoroutine(DisplayLoadingScreen(loadSceneName));
        
        
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    public void ReturnToMainMenu()
    {
        loadSceneName = "Main_Menu";
        addScreenName = "LoadingLevel";
        StartCoroutine(DisplayLoadingScreen(loadSceneName));
        Time.timeScale = 1;
    }


    public void ContinueGame()
    {
        SaveLoad.Load();
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        cameraScript = GameObject.Find("PlayerCamera").GetComponent<CameraView>();
        playerScript.enabled = true;
        cameraScript.enabled = true;
        gameoverScreen = GameObject.Find("GAMEOVER");
        playerScript.newGame = false;
        cameraScript.newGame = false;
        gameoverScreen.SetActive(false);
    }
}
