using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverButtons : MonoBehaviour {

    Button mainMenuButton, continueButton;
    GameObject gameOverCanvas;

    public bool continueButtonPushed = false;
    
    PlayerController playerScript;
    PlayerAnim animationScript;
    CameraView cameraScript;
    public bool unloadLevel = false;
    public bool fadeOut = false;

    ChangeScene changeSceneScript;
   


    private void Start()
    {

        changeSceneScript = GameObject.Find("SceneManager").GetComponent<ChangeScene>();
        gameOverCanvas = GameObject.Find("GAMEOVER");
        mainMenuButton = gameOverCanvas.transform.FindChild("Menu_Button").GetComponent<Button>();
        continueButton = gameOverCanvas.transform.FindChild("Continue_Button").GetComponent<Button>();
    }
   

    public void Continue()
    {

        SaveLoad.Load();
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        cameraScript = GameObject.Find("PlayerCamera").GetComponent<CameraView>();
        animationScript = GameObject.FindWithTag("Player").GetComponent<PlayerAnim>();

        playerScript.enabled = true;
        cameraScript.enabled = true;
        animationScript.enabled = true;
        playerScript.newGame = false;
        continueButtonPushed = true;
        StartCoroutine(changeSceneScript.UnloadLevel());
        continueButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);

    }

   

    public void ReturnToMainMenu()
    {
        changeSceneScript.unloadSceneName = "GameOver";
        changeSceneScript.loadSceneName = "Main_Menu";
        changeSceneScript.addScreenName = "LoadingLevel";
        changeSceneScript.startScene = SceneManager.GetActiveScene();
        StartCoroutine(changeSceneScript.Return(changeSceneScript.loadSceneName));
        fadeOut = true;
        mainMenuButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);

    }



   


    
}
