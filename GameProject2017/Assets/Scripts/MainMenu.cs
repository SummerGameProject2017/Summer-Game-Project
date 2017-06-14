using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    //public void StartButtonFunction()
    //{
    //    ChangeScene.startScene = SceneManager.GetActiveScene();
    //    ChangeScene.saveGame = true;
    //    ChangeScene.loadSceneName = "Junkyard_Level_VR";
    //    ChangeScene.addScreenName = "LoadingLevel";
    //    StartCoroutine(ChangeScene.DisplayLoadingScreen(ChangeScene.loadSceneName));
    //}

    //public void LoadButtonFunction()
    //{
    //    ChangeScene.startScene = SceneManager.GetActiveScene();
    //    ChangeScene.loadGame = true;
    //    ChangeScene.loadSceneName = "Junkyard_Level_VR";
    //    ChangeScene.addScreenName = "LoadingLevel";
    //    StartCoroutine(ChangeScene.DisplayLoadingScreen(ChangeScene.loadSceneName));
    //}

    
    public void ExitButton()
    {
        Application.Quit();
    }
   




}
