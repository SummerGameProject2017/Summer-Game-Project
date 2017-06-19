using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watermill_rotate : MonoBehaviour {

    public float speed = 10f;
    
    
    void Update ()
    {
        transform.Rotate(Vector3.back, speed * Time.deltaTime);
    }
}
