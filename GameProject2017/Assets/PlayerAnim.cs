using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/************************************************************************
 * 
 * Animation Controller For Player Controlled Character
 * 
 * Alex Schell
 ************************************************************************/


public class PlayerAnim : MonoBehaviour {

    // Movement animations controller
    private float inputH; 
    private float inputV;

    // Animator Component
    public static Animator Anim; 

	// Use this for initialization
	void Start () {

        Anim = GetComponent<Animator>();

        
        //set all animations false
        Anim.SetFloat("inputV" , 0f);       
        Anim.SetFloat("inputH" , 0f);
        Anim.SetBool("Jump" ,false);
        Anim.SetBool("Victory", false);
        Anim.SetBool("GetHit", false);
        Anim.SetBool("DJump", false);
        Anim.SetBool("Dead-Water", false);
        Anim.SetBool("Dead-Enemy", false);
        Anim.SetBool("Attack", false);


    }
	
	// Update is called once per frame
	void Update () {

        Anim.SetFloat("inputV", PlayerController.moveAnim.z); // Set Vertical Movement
        Anim.SetFloat("inputH", PlayerController.moveAnim.x);
       
        if (InputManager.GetButtonDown("Jump") && PlayerController.jump < 1)
        {

            Anim.SetBool("DJump", true);

        }

        if (InputManager.GetButtonUp("Jump"))
        {
            Anim.SetBool("Jump", false);
            Anim.SetBool("DJump", false);
        }

            
        
    }

    
}
