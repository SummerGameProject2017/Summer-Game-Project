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
    Animator Anim;

    PlayerController PC;
    // Use this for initialization
	void Start () {

        Anim = GetComponent<Animator>();
        PC = GetComponent<PlayerController>();
        
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

        Anim.SetFloat("inputV", PC.moveAnim.z); // Set Vertical Movement
        Anim.SetFloat("inputH", PC.moveAnim.x); // Set Horizontal Movement


        if (InputManager.GetButtonDown("Jump") && PC.jump >= 1)
        {
            Anim.SetBool("Jump", true);
        }

        if (InputManager.GetButtonDown("Jump") && PC.jump < 1)
        {
            Anim.SetBool("DJump", true);
            Anim.SetBool("Jump", false);
        }
        if (PC.isGrounded)
        {
            Anim.SetBool("Jump", false);
            Anim.SetBool("DJump", false);

        }

        if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Walk_F"))
        {
            PC.speed = 5;
        }
        else
        {
            PC.speed = 10;
        }


    }

    
}
