using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
//    public GameObject background;
    public bool loadLevel = false;
    private int loadProgress = 0;
    bool loadGame = false;
    bool saveGame = false;
    string loadSceneName;
    AsyncOperation async;
    PlayerController playerScript;
    CameraView cameraScript;
    GameObject gameoverScreen;
    private void Start()
    {
  //      background.SetActive(false);


        
        
    }


    // Update is called once per frame
    void Update () {
        SceneManager.sceneLoaded += LevelWasLoaded;//in Unity 5 have to add function call to Scene manager. scene loaded  

    }

    IEnumerator DisplayLoadingScreen(string sceneName)
    {
        
        

        loadLevel = false;
   //     background.SetActive(true);


        async = SceneManager.LoadSceneAsync(sceneName);
        

        yield return null;
       
         
           
        
            
        
        


    }


    void LevelWasLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Junkyard_Level_VR")
        {
            playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            cameraScript = GameObject.Find("PlayerCamera").GetComponent<CameraView>();
            if (loadGame == true)
            {

                loadGame = false;
                playerScript.newGame = false;
                cameraScript.newGame = false;
                SaveLoad.continueFromMain = true;
                SaveLoad.Load();
            }
            if (saveGame == true)
            {
                saveGame = false;
                playerScript.newGame = true;
                cameraScript.newGame = true;
            }
        }
    }

    public void StartButtonFunction()
    {
        SceneManager.LoadScene("LoadingLevel", LoadSceneMode.Additive);
        saveGame = true;
        loadSceneName = "Junkyard_Level_VR";
        StartCoroutine(DisplayLoadingScreen(loadSceneName));
    }

    public void LoadButtonFunction()
    {
        loadGame = true;
        loadSceneName = "Junkyard_Level_VR";
        StartCoroutine(DisplayLoadingScreen(loadSceneName));
        
        
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    public void ReturnToMainMenu()
    {
        loadSceneName = "Main_Menu";
        StartCoroutine(DisplayLoadingScreen(loadSceneName));
        Time.timeScale = 1;
    }


    public void ContinueGame()
    {
        SaveLoad.Load();
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        cameraScript = GameObject.Find("PlayerCamera").GetComponent<CameraView>();
        playerScript.enabled = true;
        cameraScript.enabled = true;
        gameoverScreen = GameObject.Find("GAMEOVER");
        playerScript.newGame = false;
        cameraScript.newGame = false;
        gameoverScreen.SetActive(false);
    }
}
