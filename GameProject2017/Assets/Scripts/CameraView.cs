using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    public Transform target;

    Vector3 offset;

    public bool newGame = true;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;

        if (newGame == true)
        {
            transform.localPosition = new Vector3(6.87f, 5.94f, -9.3f);
            transform.localRotation = Quaternion.Euler(25.7f, -51.13f, 0.0f);
        } 
        offset = transform.position - target.transform.position;
            }
    void LateUpdate()
    {

        Vector3 desiredPosition = target.transform.position + offset;
       transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 2f);

    }

}

