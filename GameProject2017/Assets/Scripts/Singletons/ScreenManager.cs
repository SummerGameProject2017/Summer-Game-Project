using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoSingleton<ScreenManager>
{


    public GameObject loadingPanel;

    //AsyncOperation newScene;


    public override void OnStart()
    {

    }

    public override void OnUpdate()
    {

    }



    public static void LoadScene(string scene)
    {

        // Adding Loading Screen on HUD
        GameObject canvas = GameObject.FindGameObjectWithTag("HUD");

        if (canvas != null)
        {
            Instantiate(instance.loadingPanel, canvas.transform);
        }

        // Load Scene on the background
        IEnumerator routine = instance.LoadSceneAsync(scene);

        instance.StartCoroutine(routine);
    }


    IEnumerator LoadSceneAsync(string scene)
    {
        AsyncOperation newScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);

        newScene.allowSceneActivation = false;

        while (newScene.progress < 0.9f)
        {
        }

        newScene.allowSceneActivation = true;

        yield return 0;
       
    }

}
