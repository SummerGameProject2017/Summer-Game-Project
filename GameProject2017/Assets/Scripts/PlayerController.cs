using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

		if (Input.GetKey(JPGameManager.GM.forward))
        {
            transform.position += Vector3.forward / 2;
        }
        if (Input.GetKey(JPGameManager.GM.backward))
        {
            transform.position += -Vector3.forward / 2;
        }
        if (Input.GetKey(JPGameManager.GM.left))
        {
            transform.position += Vector3.left / 2;
        }
        if (Input.GetKey(JPGameManager.GM.right))
        {
            transform.position += Vector3.right / 2;
        }
        if (Input.GetKeyDown(JPGameManager.GM.jump))
        {
            transform.position += Vector3.up / 2;
        }
    }


}
