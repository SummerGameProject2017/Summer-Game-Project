using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class turnOnButton : MonoBehaviour {
    Button continueButton;
    // Use this for initialization
    void Start () {
        continueButton = GameObject.Find("Continue_Button").GetComponent<Button>();
        GetComponent<Button>().interactable = false;
        StartCoroutine(WaitToPress());
    }

    IEnumerator WaitToPress()
    {
        yield return new WaitForSeconds(3);
        GetComponent<Button>().interactable = true;
        EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
    }
}
