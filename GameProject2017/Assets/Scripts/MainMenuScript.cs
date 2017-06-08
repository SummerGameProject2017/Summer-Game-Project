using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuScript : MonoBehaviour {

   
    //creates the loading transition to level1
   

    // Update is called once per frame
    

    public void StartButtonFunction()
    {

        GameObject.Find("SceneManager").GetComponent<ChangeScene>().loadLevel = true;
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
