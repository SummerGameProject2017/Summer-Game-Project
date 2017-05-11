using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{

    public float Force = 5;

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision!");
        if (col.gameObject.name == "Player")
        {
            col.gameObject.GetComponent<Rigidbody>().AddForce(0, Force, 0);
        }
    }
}
