using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuScript : MonoBehaviour {

    public Image LoadingBar;
    private AsyncOperation async;
    bool levelbutton = false;


    //creates the loading transition to level1
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        async = SceneManager.LoadSceneAsync("Level1");
        async.allowSceneActivation = false;
    }


    // Update is called once per frame
    void Update () {

        if (async != null)
        {
            LoadingBar.fillAmount = async.progress;
        }
       

        if (LoadingBar.fillAmount == 0.9f)
        {
            if (levelbutton == true)

            {
                async.allowSceneActivation = true;
            }
        }
    }

    public void StartButtonFunction()
    {

        ScreenManager.LoadScene("Level1");
    }

    public void LoadButtonFunction()
    {
        //this is where we will have to put code to load file with last checkpoint
    }

    public void ExitButton()
    {
        Application.Quit();
    }



}
