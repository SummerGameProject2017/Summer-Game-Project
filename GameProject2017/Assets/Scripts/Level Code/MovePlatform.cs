using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{


    public Vector3 pointB;
    public PlayerController PlayerScript;
    public bool onplatform;
    public Vector3 speedVector;
    private Vector3 platformmove;

    IEnumerator Start()
    {
        Vector3 pointA = transform.position;
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, 3.0f));
            transform.Rotate(0, 180, 0);
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, 3.0f));
            transform.Rotate(0, 180, 0);
        }
    }

    //Move platform back and forth from startPos to endPos
    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        float i = 0.0f;
        float rate = 0.7f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = platformmove = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            onplatform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            onplatform = false;
        }
    }

    private void Update()
    {
        if (onplatform == true)
        {
            Vector3 direction = transform.TransformDirection(speedVector );

            PlayerScript.AddMovement(direction);
        }
    }
}