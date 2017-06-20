using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class VRController : MonoBehaviour {
    public bool vive = false;
	// Use this for initialization
	void Start () {
		if (VRDevice.isPresent)
        {
            Debug.Log(VRDevice.model);
            if (VRDevice.model == "Vive MV")
            {
                Debug.Log("Vive Connected");
                    vive = true;
            }
            else
            {
                Debug.Log("Oculus Connected");
                vive = false;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
