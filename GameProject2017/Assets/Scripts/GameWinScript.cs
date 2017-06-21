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
        SceneManager.UnloadSceneAsync("Junkyard_Level_VR");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Game_Win"));
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
        changeSceneScript.startScene = SceneManager.GetActiveScene();
        changeSceneScript.saveGame = true;
        changeSceneScript.loadSceneName = "Forest_Level_VR";
        changeSceneScript.addScreenName = "LoadingLevel";
        StartCoroutine(changeSceneScript.DisplayLoadingScreen(changeSceneScript.loadSceneName));
        changeSceneScript.saveGame = true;
        GameObject.Find("Continue").SetActive(false);
    }
}
