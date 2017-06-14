using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoSingleton<ChangeScene> {
    public bool loadGame = false;
    public bool saveGame = false;
    public string loadSceneName;
    public string addScreenName;
    PlayerController playerScript;
    CameraView cameraScript;
    GameObject gameoverScreen;
    public static bool doneLoading = false;
    public bool startGame = false;
    public Scene startScene;
    public override void OnStart()
    {
        
        


        
    }


    // Update is called once per frame
    public override void OnUpdate () {
        SceneManager.sceneLoaded += LevelWasLoaded;//in Unity 5 have to add function call to Scene manager. scene loaded  
    }

    public IEnumerator DisplayLoadingScreen(string sceneName)
    {
        Scene loadScene;
        Scene newScene;

        SceneManager.LoadSceneAsync(addScreenName, LoadSceneMode.Additive);
        loadScene = SceneManager.GetSceneByName(addScreenName);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        newScene = SceneManager.GetSceneByName(sceneName);   
        
         yield return new WaitForSecondsRealtime(1);
      
        
        if (async.progress > 0.9f)
        {
            
            doneLoading = true;
     
            yield return new WaitForSeconds(1);

            SceneManager.UnloadSceneAsync(startScene);
            SceneManager.UnloadSceneAsync(loadScene);
            startGame = true;
            doneLoading = false;

        }

        yield return null;

    }

    void LevelWasLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Junkyard_Level_VR")
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(loadSceneName));
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

    //public void StartButtonFunction()
    //{
    //    startScene = SceneManager.GetActiveScene();
    //    saveGame = true;
    //    loadSceneName = "Junkyard_Level_VR";
    //    addScreenName = "LoadingLevel";
    //    StartCoroutine(DisplayLoadingScreen(loadSceneName));
    //}

    //public void LoadButtonFunction()
    //{
    //    startScene = SceneManager.GetActiveScene();
    //    loadGame = true;
    //    loadSceneName = "Junkyard_Level_VR";
    //    addScreenName = "LoadingLevel";
    //    StartCoroutine(DisplayLoadingScreen(loadSceneName));    
    //}

    //public void ExitButton()
    //{
    //    Application.Quit();
    //}
  
}
