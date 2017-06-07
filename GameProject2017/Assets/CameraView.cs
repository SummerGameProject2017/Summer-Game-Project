using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    public Transform target;

    Vector3 offset;

    public float damping = 2;
    public bool ChangeCameraPositionForDevPurposes;
    // Use this for initialization
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;

        if (ChangeCameraPositionForDevPurposes == false)
        {
            transform.localPosition = new Vector3(7.9f, 7.9f, -10.7f);
            transform.localRotation = Quaternion.Euler(24.043f, -51.16f, -1.258f);
        } 
        offset = transform.position - target.transform.position;
            }
    void LateUpdate()
    {

        Vector3 desiredPosition = target.transform.position + offset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
        //transform.LookAt(target);

    }

}

