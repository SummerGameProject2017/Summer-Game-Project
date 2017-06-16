using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWinScript : MonoBehaviour
{
    ChangeScene changeSceneScript;
    Text text1;
    Text text2;
    // Use this for initialization
    void Start()
    {
        changeSceneScript = GameObject.Find("SceneManager").GetComponent<ChangeScene>();
        text1 = transform.FindChild("CollectionText").GetComponent<Text>();
        text2 = transform.FindChild("OilsworthText").GetComponent<Text>();

        text1.text = "You have collected " + Player.Instance.gear + "/37 gears!";
        if (Player.Instance.robot > 0)
        {
            text2.text = "You saved Oilsworth!";
        }
        else
        {
            text2.text = "You did not save Oilsworth!";
        }
    }

    // Update is called once per frame

    public void ContinueButton()
    {
        changeSceneScript.unloadSceneName = "Game_Win";
        changeSceneScript.loadSceneName = "Main_Menu";
        changeSceneScript.addScreenName = "LoadingLevel";
        changeSceneScript.startScene = SceneManager.GetActiveScene();
        StartCoroutine(changeSceneScript.Return(changeSceneScript.loadSceneName));
       
        GameObject.Find("Continue").SetActive(false);
    }
}
