using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlScript : MonoBehaviour {
    Transform menuPanel;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;

    bool waitingForKey;

	// Use this for initialization
	void Start () {
        menuPanel = transform.FindChild("Panel");
        menuPanel.gameObject.SetActive(false);
        waitingForKey = false;

        for (int i = 0; i < 5; i++)
        {
            if (menuPanel.GetChild(i).name == "ForwardKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = JPGameManager.GM.forward.ToString();
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
