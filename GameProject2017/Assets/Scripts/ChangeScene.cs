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

    public override void OnStart()
    {
        newGameButton = GameObject.Find("New Game");  
        EventSystem.current.firstSelectedGameObject = newGameButton;
         
    }


    // Update is called once per frame
    public override void OnUpdate() {
        SceneManager.sceneLoaded += LevelWasLoaded;//in Unity 5 have to add function call to Scene manager. scene loaded  

        if (InputManager.GetButtonDown("Pause"))
        {
            StartCoroutine(PauseMenu());
            Time.timeScale = 0;
            
        }
    }

    public IEnumerator DisplayLoadingScreen(string sceneName)
    {
        

        if (!SceneManager.GetSceneByName(addScreenName).isLoaded)
        {
            async1 = SceneManager.LoadSceneAsync(addScreenName, LoadSceneMode.Additive);
            loadScene = SceneManager.GetSceneByName(addScreenName);
        }
        
        yield return new WaitForSecondsRealtime(1);

        if (async1.isDone)
        {
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                async2 = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }

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

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
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
    IEnumerator PauseMenu()
    {
        SceneManager.LoadScene("PauseScene", LoadSceneMode.Additive);

        yield return new WaitForSecondsRealtime(1);


        yield return null;
    }


    void LevelWasLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Junkyard_Level_VR")
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(loadSceneName));
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

    }

    
  
}
