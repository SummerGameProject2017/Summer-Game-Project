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
    CameraView cameraScript;
    public bool unloadLevel = false;
    public bool fadeOut = false;

    GameOver gameOverScript;



    private void Start()
    {
        
        gameOverScript = GameObject.Find("SceneManager").GetComponent<GameOver>();
        gameOverCanvas = GameObject.Find("GAMEOVER");
        mainMenuButton = gameOverCanvas.transform.FindChild("Menu_Button").GetComponent<Button>();
        continueButton = gameOverCanvas.transform.FindChild("Continue_Button").GetComponent<Button>();
        EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
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
        StartCoroutine(gameOverScript.UnloadLevel());
    }

   

    public void ReturnToMainMenu()
    {
        gameOverScript.loadSceneName = "Main_Menu";
        gameOverScript.addScreenName = "LoadingLevel";
        gameOverScript.startScene = SceneManager.GetActiveScene();
        StartCoroutine(gameOverScript.Return(gameOverScript.loadSceneName));
        fadeOut = true;
        mainMenuButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);

    }



    private void Update()
    {
        if (InputManager.GetAxis("Horizontal") < 0)
        {
            mainMenuButton.Select();

        }
        if (InputManager.GetAxis("Horizontal") > 0)
        {
            continueButton.Select();
        }



    }


}
