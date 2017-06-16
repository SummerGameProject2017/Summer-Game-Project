using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour {
    PlayerController playerScript;
    PlayerAnim animationScript;
    ChangeScene changeSceneScript;
    GameObject continueButton, quitButton;

    public void Start()
    {
        quitButton = GameObject.Find("Quit Button");
        continueButton = GameObject.Find("Continue");
        changeSceneScript = GameObject.Find("SceneManager").GetComponent<ChangeScene>();
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        animationScript = GameObject.FindWithTag("Player").GetComponent<PlayerAnim>();
    }

    public void Continue()
    {
        playerScript.enabled = true;
        animationScript.enabled = true;
        Time.timeScale = 1;
        StartCoroutine(changeSceneScript.UnloadImmediately());
    }

    public void QuitToMain()
    {
        Time.timeScale = 1;
        changeSceneScript.unloadSceneName = "PauseScene";
        changeSceneScript.loadSceneName = "Main_Menu";
        changeSceneScript.addScreenName = "LoadingLevel";
        changeSceneScript.startScene = SceneManager.GetActiveScene();
        StartCoroutine(changeSceneScript.Return(changeSceneScript.loadSceneName));
        continueButton.SetActive(false);
        quitButton.SetActive(false);
    }
	
}
