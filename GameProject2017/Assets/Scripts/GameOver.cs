using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
   
    public string addScreenName;
    public string loadSceneName;
    PlayerController playerScript;
    CameraView cameraScript;
    public bool unloadLevel = false;
 //   public bool continueButtonPushed = false;
    public bool fadeOut = false;
    Scene loadScene;
    public Scene startScene;
    public bool changeCamera = false;



   
    public IEnumerator DisplayLoadingScreen(string sceneName)
    {

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        loadScene = SceneManager.GetSceneByName(sceneName);
        yield return new WaitForSecondsRealtime(1);


        yield return null;
    }

    public IEnumerator Return(string sceneName)
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

    
    public IEnumerator UnloadLevel()
    {

        yield return new WaitForSeconds(2);
        SceneManager.UnloadSceneAsync("GameOver");
        ChangeScene.doneLoading = false;

    }


 


    


}
