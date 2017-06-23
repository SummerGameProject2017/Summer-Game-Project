using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseSceenCamearPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.position = GameObject.Find("PlayerCamera").transform.position;
        transform.rotation = GameObject.Find("PlayerCamera").transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
