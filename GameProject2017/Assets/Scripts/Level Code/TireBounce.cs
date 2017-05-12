using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireBounce : MonoBehaviour
{
    public Vector3 Speed;

    // Use this for initialization
    void Start ()
    {	
	}
	
	// Update is called once per frame
	void Update ()
    {
	}
    
    public void OnTriggerStay(Collider col)
    {
        //If player is in collider and jumps then make him jump higher
        if ((col.gameObject.tag == "Player") && (Input.GetKeyDown(JPGameManager.GM.jump)))
        {
            CharacterController ctrl = col.gameObject.GetComponent(typeof(CharacterController)) as CharacterController;
            if (ctrl)
            {
                ctrl.Move(Speed);
            }
        }
    }
}
