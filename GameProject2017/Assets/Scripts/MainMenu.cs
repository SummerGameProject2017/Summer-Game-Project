using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    ChangeScene changeSceneScript;

    public void StartButtonFunction()
    {
        changeSceneScript = GameObject.Find("SceneManager").GetComponent<ChangeScene>();
        changeSceneScript.startScene = SceneManager.GetActiveScene();
        changeSceneScript.saveGame = true;
        changeSceneScript.loadSceneName = "Junkyard_Level_VR";
        changeSceneScript.addScreenName = "LoadingLevel";
        StartCoroutine(changeSceneScript.DisplayLoadingScreen(changeSceneScript.loadSceneName));
    }

    public void LoadButtonFunction()
    {
        changeSceneScript = GameObject.Find("SceneManager").GetComponent<ChangeScene>();
        changeSceneScript.startScene = SceneManager.GetActiveScene();
        changeSceneScript.loadGame = true;
        changeSceneScript.loadSceneName = "Junkyard_Level_VR";
        changeSceneScript.addScreenName = "LoadingLevel";
        StartCoroutine(changeSceneScript.DisplayLoadingScreen(changeSceneScript.loadSceneName));
    }


    public void ExitButton()
    {
        Application.Quit();
    }
   




}
