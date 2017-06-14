using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenCamearPosition : MonoBehaviour {
    GameOver gameOverScript;
	// Use this for initialization
	void Start () {
        gameOverScript = GameObject.Find("GAMEOVER").GetComponent<GameOver>();
        transform.position = GameObject.Find("PlayerCamera").transform.position;
        transform.rotation = GameObject.Find("PlayerCamera").transform.rotation;

    }
	
	// Update is called once per frame
	void Update () {
        if (gameOverScript.continueButtonPushed == true)
        {
            transform.position = GameObject.Find("PlayerCamera").transform.position;
            transform.rotation = GameObject.Find("PlayerCamera").transform.rotation;
        }
        if (gameOverScript.changeCamera == true)
        {
            gameObject.SetActive(false);
        }

    }
}
