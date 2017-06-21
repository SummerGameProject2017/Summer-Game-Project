using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoSingleton<ChangeScene> {
    public bool loadGame = false;
    public bool saveGame = false;
    public string loadSceneName;
    public string addScreenName;
    PlayerController playerScript;
    PlayerAnim animationScript;
    CameraView cameraScript;
    GameObject gameoverScreen;
    public static bool doneLoading = false;
    public bool startGame = false;
    public Scene startScene;
    GameObject myEventSystem;
    GameObject newGameButton;
    AsyncOperation async1;
    AsyncOperation async2;
    Scene loadScene;
    public string unloadSceneName;

    public bool unloadLevel = false;
    public bool fadeOut = false;
    public bool changeCamera = false;
    GameObject continueGameButton;
    GameObject player;
    GameObject playerCamera;
    public override void OnStart()
    {
       
        newGameButton = GameObject.Find("New Game");  
        EventSystem.current.firstSelectedGameObject = newGameButton;
         
    }


    // Update is called once per frame
    public override void OnUpdate() {
        SceneManager.sceneLoaded += LevelWasLoaded;//in Unity 5 have to add function call to Scene manager. scene loaded  

        
    }

    public IEnumerator DisplayLoadingScreen(string sceneName)
    {
        

      
            async1 = SceneManager.LoadSceneAsync(addScreenName, LoadSceneMode.Additive);
            loadScene = SceneManager.GetSceneByName(addScreenName);
        
        
        yield return new WaitForSecondsRealtime(1);

        if (async1.isDone)
        {
           
                async2 = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
           

            yield return new WaitForSecondsRealtime(1);


            if (async2.progress > 0.9f)
            {

                doneLoading = true;

                yield return new WaitForSeconds(1);

                SceneManager.UnloadSceneAsync(startScene);
                SceneManager.UnloadSceneAsync(loadScene);
                startGame = true;
                doneLoading = false;
                playerScript.enabled = true;
            }
        }
        yield return null;

    }


    public IEnumerator DisplayGameOverScreen(string sceneName)
    {

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        loadScene = SceneManager.GetSceneByName(sceneName);
        yield return new WaitForSecondsRealtime(1);


        yield return null;
    }

    public IEnumerator DisplayGameWinScreen()
    {
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = false;
        SceneManager.LoadScene("Game_Win", LoadSceneMode.Additive);
        loadScene = SceneManager.GetSceneByName("Game_Win");
        yield return new WaitForSecondsRealtime(1);
        

        yield return null;
    }

    public IEnumerator Return(string sceneName)
    {

        animationScript.Anim.enabled = false;
        async1 = SceneManager.LoadSceneAsync(addScreenName, LoadSceneMode.Additive);
        loadScene = SceneManager.GetSceneByName(addScreenName);

        yield return new WaitForSecondsRealtime(1);

        if (async1.isDone)
        { 
            async2 = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            yield return new WaitForSecondsRealtime(1);


            SceneManager.UnloadSceneAsync(startScene);
            if (async2.isDone)
            {
                changeCamera = true;
                ChangeScene.doneLoading = true;
                fadeOut = true;

                yield return new WaitForSeconds(1);

                SceneManager.UnloadSceneAsync(loadScene);

                SceneManager.UnloadSceneAsync(unloadSceneName);

                ChangeScene.doneLoading = false;
                changeCamera = false;
                
            }

            yield return null;
        }
    }
    public IEnumerator UnloadLevel()
    {

        yield return new WaitForSeconds(2);
        SceneManager.UnloadSceneAsync("GameOver");
        ChangeScene.doneLoading = false;

    }
    public IEnumerator UnloadImmediately()
    {       
        SceneManager.UnloadSceneAsync("PauseScene");
        ChangeScene.doneLoading = false;
        yield return null;

    }
    public IEnumerator PauseMenu()
    {
        SceneManager.LoadScene("PauseScene", LoadSceneMode.Additive);

        yield return new WaitForSecondsRealtime(1);


        yield return null;
    }


    void LevelWasLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Junkyard_Level_VR")
        {
            playerCamera = GameObject.Find("PlayerCamera");
            player = GameObject.FindWithTag("Player");
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(loadSceneName));
            playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            animationScript = GameObject.FindWithTag("Player").GetComponent<PlayerAnim>();
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
                player.transform.localPosition = new Vector3(124.0f, -93.0f, -247.7f);
                saveGame = false;
                playerScript.newGame = true;
                playerCamera.transform.localPosition = new Vector3(6.87f, 5.94f, -9.3f);
                playerCamera.transform.localRotation = Quaternion.Euler(25.7f, -51.13f, 0.0f);
            }

        }
        if (scene.name == "Main_Menu")
        {
            continueGameButton = GameObject.Find("Continue");
            EventSystem.current.SetSelectedGameObject(continueGameButton);
        }
        if (scene.name == "PauseScene")
        {
            continueGameButton = GameObject.Find("Continue");
            EventSystem.current.SetSelectedGameObject(continueGameButton);
        }
        if (scene.name == "Game_Win")
        {
            continueGameButton = GameObject.Find("Continue");
            EventSystem.current.SetSelectedGameObject(continueGameButton);
        }
        if (scene.name == "Forest_Level_VR")
        {
            playerCamera = GameObject.Find("PlayerCamera");
            player = GameObject.FindWithTag("Player");
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Forest_Level_VR"));
            playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            animationScript = GameObject.FindWithTag("Player").GetComponent<PlayerAnim>();
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
                Debug.Log("new Game");
                player.transform.localPosition = new Vector3(-342.48f, 310.95f, 59.65f);
                playerCamera.transform.localPosition = new Vector3(182.09f, 14.33f, -7.91f);
                playerCamera.transform.localRotation = Quaternion.Euler(6.933001f, -175.238f, 0f);
                saveGame = false;
                playerScript.newGame = true;
                cameraScript.newGame = true;
            }
        }

    }

    
  
}
