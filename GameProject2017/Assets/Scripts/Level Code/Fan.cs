using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public float Force;
    public float groundForce;
    public float enterForce;

    public void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            PlayerController ctrl = col.gameObject.GetComponent<PlayerController>();
            ctrl.AddMovement(new Vector3(0, enterForce * Time.deltaTime, 0));

        }
    }

    public void OnTriggerStay(Collider col)
    {
        //If player is in collider make him fly upwards
        if (col.gameObject.tag == "Player")
        {
            PlayerController ctrl = col.gameObject.GetComponent<PlayerController>();
            if (ctrl)
            {
                if (ctrl.isGrounded)
                {
                    ctrl.AddMovement(new Vector3(0, groundForce * Time.deltaTime, 0));
                }

                ctrl.AddMovement(new Vector3(0, Force * Time.deltaTime, 0));
                
            }

        }
    }
}
