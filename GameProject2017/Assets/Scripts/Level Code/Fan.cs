using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public Vector3 Force;

    public void OnTriggerStay(Collider col)
    {
        //If player is in collider make him fly upwards
        if (col.gameObject.tag == "Player")
        {
            CharacterController ctrl = col.gameObject.GetComponent(typeof(CharacterController)) as CharacterController;
            if (ctrl)
            {
                ctrl.Move(Force);
            }
        }
    }
}
