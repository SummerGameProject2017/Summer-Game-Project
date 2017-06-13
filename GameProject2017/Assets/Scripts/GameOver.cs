using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    PlayerController playerScript;
    CameraView cameraScript;
    public bool unloadLevel = false;
    public bool continueButtonPushed = false;
    Scene loadScene;
    public IEnumerator DisplayLoadingScreen(string sceneName)
    {
        
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        loadScene = SceneManager.GetSceneByName(sceneName);
        yield return new WaitForSecondsRealtime(1);
            
       
        yield return null;
    }
    public void Continue()
    {
            continueButtonPushed = true;
            SaveLoad.Load();
            playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            cameraScript = GameObject.Find("PlayerCamera").GetComponent<CameraView>();
            
            playerScript.enabled = true;
            cameraScript.enabled = true;
            playerScript.newGame = false;
            cameraScript.newGame = false;
            
        ChangeScene.doneLoading = true;
    }

    public IEnumerator UnloadLevel()
    {
        
            yield return new WaitForSeconds(2);
        SceneManager.UnloadSceneAsync("GameOver");
        ChangeScene.doneLoading = false;
            
    }
}
