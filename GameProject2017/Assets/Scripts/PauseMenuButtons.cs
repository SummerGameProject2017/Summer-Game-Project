using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuButtons : MonoBehaviour {
    PlayerController playerScript;
    PlayerAnim animationScript;
    ChangeScene changeSceneScript;

    public void Start()
    {
        changeSceneScript = GameObject.Find("SceneManager").GetComponent<ChangeScene>();
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        animationScript = GameObject.FindWithTag("Player").GetComponent<PlayerAnim>();
    }

    public void Continue()
    {
        playerScript.enabled = true;
        animationScript.enabled = true;
        Time.timeScale = 1;
        StartCoroutine(changeSceneScript.UnloadImmediately());
    }

    public void QuitToMain()
    {

    }
	
}
