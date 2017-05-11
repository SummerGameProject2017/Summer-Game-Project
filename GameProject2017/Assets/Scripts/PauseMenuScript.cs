using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{

    GameObject[] pauseObjects;

    //objects and bool for loading main menu via ASYNC
    public Image LoadingBar;
    private AsyncOperation async;
    bool levelbutton = false;
    Animator anim;
    //end of ASYNC

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        anim = GetComponent<Animator>();
        pauseObjects = GameObject.FindGameObjectsWithTag("GamePaused");
        hidePaused();



    }
    //ASYNC
    IEnumerator one()
    {
        yield return new WaitForSeconds(0.1f);
        async = SceneManager.LoadSceneAsync("Level1");
        async.allowSceneActivation = false;
        Debug.Log("fffff");
    }
    //ENF of ASYNC enumerator

    // Update is called once per frame
    void Update()
    {

        //uses the escape button to pause the game
        if (InputManager.GetButtonDown("Cancel"))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showPaused();
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                hidePaused();
            }
        }

        //for ASYNC
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
        //END ASYNC
    }

    //controls the pausing of the scene - function used for Play button and escape
    public void pauseGameControl()
    {



        anim.SetBool("isPaused", true);


            if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    //shows objects with GamePaused tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with GamePaused tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //loads main menu
    public void LoadLevel()
    {
        SceneManager.LoadScene("Main Menu");
    }

    //for restarting at checkpoint 
    public void RestartCheckpoint()
    {
        //code here to load to last checkpoint
    }

    //to activate ASYNC (currently does not work) 
    //using public void LoadLevel() at this time to load main menu
    public void LoadMainMenu()
    {
        levelbutton = true;
        Debug.Log("ddddd");
    }

}
