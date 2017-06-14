using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    string addScreenName;
    string loadSceneName;
    PlayerController playerScript;
    CameraView cameraScript;
    public bool unloadLevel = false;
    public bool continueButtonPushed = false;
    public bool fadeOut = false;
    Scene loadScene;
    Scene startScene;
    public bool changeCamera = false;
    public IEnumerator DisplayLoadingScreen(string sceneName)
    {
        
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        loadScene = SceneManager.GetSceneByName(sceneName);
        yield return new WaitForSecondsRealtime(1);
            
       
        yield return null;
    }

    IEnumerator Return(string sceneName)
    {
       
        Scene newScene;

        SceneManager.LoadSceneAsync(addScreenName, LoadSceneMode.Additive);
        loadScene = SceneManager.GetSceneByName(addScreenName);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        newScene = SceneManager.GetSceneByName(sceneName);

        yield return new WaitForSecondsRealtime(1);
        
        
        SceneManager.UnloadSceneAsync(startScene);
        if (async.isDone)
        {
            changeCamera = true;
            ChangeScene.doneLoading = true;
            fadeOut = true;

            yield return new WaitForSeconds(1);

            SceneManager.UnloadSceneAsync(loadScene);

            SceneManager.UnloadSceneAsync("GameOver");
            
            ChangeScene.doneLoading = false;
            changeCamera = false;
        }

        yield return null;

    }

    public void Continue()
    {
        
            SaveLoad.Load();
            playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            cameraScript = GameObject.Find("PlayerCamera").GetComponent<CameraView>();
            
            playerScript.enabled = true;
            cameraScript.enabled = true;
            playerScript.newGame = false;
            cameraScript.newGame = false;
            continueButtonPushed = true;
        StartCoroutine(UnloadLevel());
    }

    public IEnumerator UnloadLevel()
    {
        
            yield return new WaitForSeconds(2);
        SceneManager.UnloadSceneAsync("GameOver");
        ChangeScene.doneLoading = false;
            
    }


    public void ReturnToMainMenu()
    {
        loadSceneName = "Main_Menu";
        addScreenName = "LoadingLevel";
        startScene = SceneManager.GetActiveScene();
        StartCoroutine(Return(loadSceneName));
        fadeOut = true;


    }
}
