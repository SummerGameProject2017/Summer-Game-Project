using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour {

	// Use this for initialization
	void Start () {

        

        // Kills the game object in 5 seconds after loading the object
        Destroy(gameObject, 5.0f);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
