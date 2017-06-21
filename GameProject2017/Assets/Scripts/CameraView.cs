using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class CameraView : MonoBehaviour
{
    public Transform target;
    string VRName;
    Vector3 offset;
    public float speed = 2;
    public bool newGame = true;
    void Start()
    {
        if (VRDevice.isPresent)
        {
            VRName = VRDevice.model;   
        
        if (VRName.Contains("Vive"))
        {
            speed = 3.5f;
        }
        else
            speed = 2;
        }
        target = GameObject.FindWithTag("Player").transform;

        
        offset = transform.position - target.transform.position;
            }
    void LateUpdate()
    {
        Vector3 desiredPosition = target.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * speed);

    }

}

