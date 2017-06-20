using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class VRController : MonoBehaviour {

    public string VRDeviceName;
    public bool vive = false;
    // Use this for initialization

    static readonly float DEADZONE = 0.3f;


    void Start () {
		if (VRDevice.isPresent)
        {
            VRDeviceName = VRDevice.model;
            if (VRDeviceName.Contains("Vive"))
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
    void Update()
    {
        if (vive == true)
        {

        }
    }

     
}
