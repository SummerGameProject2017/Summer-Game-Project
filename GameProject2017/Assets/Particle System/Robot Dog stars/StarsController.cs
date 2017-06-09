using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsController : MonoBehaviour {
	// Use this for initialization
	void Start () {

        Destroy(this.gameObject, 3);
     //   transform.position = new Vector3(transform.position.x,transform.position.y + 3, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
