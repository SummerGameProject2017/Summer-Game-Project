using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(WaitToQuit());
	}
	
	// Update is called once per frame
	IEnumerator WaitToQuit()
    {
        yield return new WaitForSeconds(10);
        Application.Quit();
    }
}
