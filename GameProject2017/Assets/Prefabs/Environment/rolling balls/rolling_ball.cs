using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rolling_ball : MonoBehaviour
{

    // public float speed = 10f;
    public float y = 2.0f;
    //public float x = 0.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.up, speed * Time.deltaTime);    

        transform.Rotate(new Vector3(0.0f, y, 0.0f * Time.deltaTime));

    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "ball 2")
        {
            //Destroy(col.gameObject);
            Debug.Log("WWWWWWWWWWWW");
        }
    }
}