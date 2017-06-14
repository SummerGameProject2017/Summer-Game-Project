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
    Scene loadScene;
    Scene startScene;
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
        
        SceneManager.UnloadSceneAsync("GameOver");
        SceneManager.UnloadSceneAsync(startScene);
        if (async.progress > 0.9f)
        {
           
            ChangeScene.doneLoading = true;
            Debug.Log("UNLOAD");            

            SceneManager.UnloadSceneAsync(loadScene);

            
            ChangeScene.doneLoading = false;
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
        


    }
}
