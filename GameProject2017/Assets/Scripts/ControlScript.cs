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
    void Start() {
        menuPanel = transform.FindChild("Panel");
        menuPanel.gameObject.SetActive(false);
        waitingForKey = false;

        for (int i = 0; i < 5; i++)
        {
            if (menuPanel.GetChild(i).name == "ForwardKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = JPGameManager.GM.forward.ToString();
            }
            else if (menuPanel.GetChild(i).name == "BackwardKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = JPGameManager.GM.backward.ToString();
            }
            else if (menuPanel.GetChild(i).name == "LeftKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = JPGameManager.GM.left.ToString();
            }
            else if (menuPanel.GetChild(i).name == "RightKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = JPGameManager.GM.right.ToString();
            }
            else if (menuPanel.GetChild(i).name == "JumpKey")
            {
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = JPGameManager.GM.jump.ToString();
            }

        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuPanel.gameObject.activeSelf)
        {
            menuPanel.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuPanel.gameObject.activeSelf)
        {
            menuPanel.gameObject.SetActive(false);
        }
    }

    private void OnGUI()
    {
        keyEvent = Event.current;

        if (keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }
    public void StartAssignment(string keyname)
    {
        if (!waitingForKey)
        {
            StartCoroutine(AssignKey(keyname));
        }
    }
    public void SendText(Text text)
    {
        buttonText = text;
    }
    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
            {
            yield return null;
            }
    }

    public IEnumerator AssignKey(string keyname)
    {
        waitingForKey = true;
        yield return WaitForKey();

        switch(keyname)
        {
            case "forward":
                JPGameManager.GM.forward = newKey;
                buttonText.text = JPGameManager.GM.forward.ToString();
                PlayerPrefs.SetString("forwardKey", JPGameManager.GM.forward.ToString());
                break;
            case "backward":
                JPGameManager.GM.backward = newKey;
                buttonText.text = JPGameManager.GM.backward.ToString();
                PlayerPrefs.SetString("backwardKey", JPGameManager.GM.backward.ToString());
                break;
            case "left":
                JPGameManager.GM.left = newKey;
                buttonText.text = JPGameManager.GM.left.ToString();
                PlayerPrefs.SetString("leftKey", JPGameManager.GM.left.ToString());
                break;
            case "right":
                JPGameManager.GM.right = newKey;
                buttonText.text = JPGameManager.GM.right.ToString();
                PlayerPrefs.SetString("rightKey", JPGameManager.GM.right.ToString());
                break;
            case "jump":
                JPGameManager.GM.jump = newKey;
                buttonText.text = JPGameManager.GM.jump.ToString();
                PlayerPrefs.SetString("jumpKey", JPGameManager.GM.jump.ToString());
                break;
        }
        yield return null;
    }


}
