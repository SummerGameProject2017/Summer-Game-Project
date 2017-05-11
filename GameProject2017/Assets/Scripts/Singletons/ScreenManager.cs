using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoSingleton<ScreenManager>
{


    public GameObject loadingPanel;


    public override void OnStart()
    {

    }

    public override void OnUpdate()
    {

    }



    public static void LoadScene(UnityEngine.SceneManagement.Scene scene)
    {


        GameObject canvas = GameObject.FindGameObjectWithTag("HUD");

        if (canvas != null)
        {

            Instantiate(instance.loadingPanel, canvas.transform);

        }


        IEnumerator routine = instance.LoadSceneAsync(scene);

        instance.StartCoroutine(routine);

    }


    IEnumerator LoadSceneAsync(UnityEngine.SceneManagement.Scene scene)
    {
        yield return new WaitForSeconds(0.1f);

        AsyncOperation newScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene.name);

        newScene.allowSceneActivation = false;

        while (!newScene.isDone)
        {

            /**
             *  DO SOMETHING HERE
             */


            if (newScene.progress == 0.9f)
            {
                newScene.allowSceneActivation = true;
            }
        }

        yield return null;
       
    }

}
