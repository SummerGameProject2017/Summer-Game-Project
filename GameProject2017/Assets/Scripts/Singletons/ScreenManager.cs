using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenManager : MonoSingleton<ScreenManager>
{

    private string loadingScene = "Loading";


    [Header("Fade Panel")]
    public GameObject loadingPanel;
    [Range(0.1f, 5.5f)]
    public float fadeTime = 1.0f;
    

    private GameObject actualSceneCanvas;


    public override void OnStart()
    {

    }

    public override void OnUpdate()
    {

    }



    public static void LoadScene(string scene)
    {
        // Load Scene on the background
        IEnumerator routine = instance.LoadSceneAsync(scene);

        instance.StartCoroutine(routine);
    }


    IEnumerator LoadSceneAsync(string scene)
    {

        Scene actualScene = SceneManager.GetActiveScene();
        Scene loadingScene;// = SceneManager.GetSceneByName(this.loadingScene);
        Scene newScene;// = SceneManager.GetSceneByName(scene);

        actualSceneCanvas = GameObject.FindGameObjectWithTag("HUD");

        if (actualSceneCanvas != null)
        {
            // Add the loading screen to the canvas
            GameObject go = Instantiate(instance.loadingPanel, actualSceneCanvas.transform) as GameObject;

            // Set it to transparent
            go.GetComponent<CanvasRenderer>().SetAlpha(0.0f);

            // Fade the loading screen in
            yield return StartCoroutine(FadeIn(go));
            // yield return StartCoroutine("FadeIn");

            // Clear Loading Panel from old scene
            go = null;

            // Add the Loading Scene
            yield return StartCoroutine(LoadNewScene(this.loadingScene, LoadSceneMode.Additive));

            // Get Loading Scene
            loadingScene = SceneManager.GetSceneByName(this.loadingScene);

            // Wait for the Loading screen to be fully loaded
            while (!loadingScene.isLoaded)
            {
                yield return new WaitForEndOfFrame();
            }

            // Load new Scene
            yield return StartCoroutine(LoadNewScene(scene, LoadSceneMode.Additive));

            // Get new Scene
            newScene = SceneManager.GetSceneByName(scene);

            // Wait for the new screen to be fully loaded
            while (!newScene.isLoaded)
            {
                yield return new WaitForEndOfFrame();
            }
            
            // Wait 1 second for all Asyncronous operations to be completed
            // Without this, the system will crash
            yield return new WaitForSecondsRealtime(1);


            // Get all main objects from new Scene
            GameObject[] root = newScene.GetRootGameObjects();

            // Look for the HUD to add a black panel for Fade out effect
            foreach (GameObject item in root)
            {
                if (item.tag == "HUD")
                {
                    // Add the loading screen to the canvas
                    go = Instantiate(instance.loadingPanel, item.transform) as GameObject;

                    // Set it to full alpha
                    go.GetComponent<CanvasRenderer>().SetAlpha(1.0f);
                }
            }

            // Get rid of the fist scene and loading scene
            SceneManager.UnloadSceneAsync(actualScene);
            SceneManager.UnloadSceneAsync(loadingScene);

            // Fade out on new screen
            if(go != null )
                yield return StartCoroutine(FadeOut(go));

        }

        yield return 0;

    }


    IEnumerator FadeIn(GameObject obj)
    {
        float actualTime = 0;
        float finalTime = fadeTime;

        float alpha = obj.GetComponent<CanvasRenderer>().GetAlpha();

        while (alpha < 1.0f)
        {
            actualTime += Time.deltaTime;

            alpha = actualTime / finalTime;

            obj.GetComponent<CanvasRenderer>().SetAlpha(alpha);

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }


    IEnumerator FadeOut(GameObject obj)
    {

        float actualTime = 0;
        float finalTime = fadeTime;

        float alpha = obj.GetComponent<CanvasRenderer>().GetAlpha();
        while (alpha > 0.0f)
        {
            actualTime += Time.deltaTime;

            alpha = 1 - (actualTime / finalTime);

            obj.GetComponent<CanvasRenderer>().SetAlpha(alpha);

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }


    IEnumerator LoadNewScene(string scene, LoadSceneMode mode)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(scene, mode);

        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
        }

        async.allowSceneActivation = true;

        yield return 0;
    }

}
