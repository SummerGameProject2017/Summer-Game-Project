using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    ChangeScene changeSceneScript;
    GameObject loadButton;
    GameObject newGameButton;
    GameObject quitButton;
    public string loadLevelName;
    bool canLoadExitScreen = true;

    private void Start()
    {
         changeSceneScript = GameObject.Find("SceneManager").GetComponent<ChangeScene>();
        loadButton = GameObject.Find("Continue");
        newGameButton = GameObject.Find("New Game");
        quitButton = GameObject.Find("Quit");
    }

    public void StartButtonFunction()
    {
        changeSceneScript.startScene = SceneManager.GetActiveScene();
        changeSceneScript.saveGame = true;
        changeSceneScript.loadSceneName = "Junkyard_Level_VR";
        changeSceneScript.addScreenName = "LoadingLevel";
        loadButton.SetActive(false);
        newGameButton.SetActive(false);
        quitButton.SetActive(false);
        StartCoroutine(changeSceneScript.DisplayLoadingScreen(changeSceneScript.loadSceneName));

    }

    public void LoadButtonFunction()
    {
        changeSceneScript.startScene = SceneManager.GetActiveScene();
        changeSceneScript.loadGame = true;
        changeSceneScript.loadSceneName = SaveLoad.GetLevelName();
        changeSceneScript.addScreenName = "LoadingLevel";
        loadButton.SetActive(false);
        newGameButton.SetActive(false);
        quitButton.SetActive(false);
        StartCoroutine(changeSceneScript.DisplayLoadingScreen(changeSceneScript.loadSceneName));
    }


    public void ExitButton()
    {
        if (canLoadExitScreen == true)
        {
            canLoadExitScreen = false;
            SceneManager.LoadSceneAsync("Splash_Screen");
        }
    }


   


}
