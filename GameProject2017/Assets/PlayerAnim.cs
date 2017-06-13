﻿using System.Collections;
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
    public Animator Anim;

    //Bools
    public bool attacking = false;
    public bool inTransition = false;

    // Refernced scripts
    CameraView cameraScript;
    PlayerController PC;
    GameOver DeadScript;



    // Use this for initialization
    void Start () {
        cameraScript = GameObject.Find("PlayerCamera").GetComponent<CameraView>();
        DeadScript = GetComponent<GameOver>();
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

        Anim.SetFloat("inputV", PC.v); // Set Vertical Movement
        Anim.SetFloat("inputH", PC.h); // Set Horizontal Movement

        if (Player.Instance.lives > 0)
        {
            if (InputManager.GetButtonDown("Jump") && PC.jump >= 1 && PC.isActiveAndEnabled && PC.isTalking == false)
            {
                Anim.SetBool("Jump", true);
            }
            

            if (InputManager.GetButtonDown("Jump") && PC.jump < 1)
            {
                Debug.Log("DJump");
                Anim.SetBool("DJump", true);
            }
            if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9 ) //before the animation is done set its bool to be false
            {
                if(Anim.GetCurrentAnimatorStateInfo(0).IsTag("Jump")
                    || Anim.GetCurrentAnimatorStateInfo(0).IsTag("DJump"))

                    Anim.SetBool("Jump", false);

                if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("DJump"))

                    Anim.SetBool("DJump", false);

            }
           
            
            if (InputManager.GetButtonDown("Attack"))
            {
                if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7)
                    {
                        Anim.Play("Attack", -1, 0);
                    }

                }
                else
                {
                    Anim.Play("Attack", -1, 0);
                }

            }

            if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                attacking = true;
            }
            else
            {
                attacking = false;
            }

        }


        if (!PC.isActiveAndEnabled)
        {
            Anim.SetBool("Jump", false);
            Anim.Play("Idle", -1, 0);
        }
        if (Anim.IsInTransition(0))
        {
            inTransition = true;

        }
        if(!Anim.IsInTransition(0))
        {
            inTransition = false;
        }

    }


    private IEnumerator OnTriggerEnter(Collider other)
    {
        //if the player falls in water play the falling in water animation then reset player to last save position
        if (other.gameObject.tag == "Water")
        {

            cameraScript.enabled = false;
            Anim.Play("Death-Water", -1, 0);
            yield return new WaitForSeconds(1);
            PC.enabled = false;
            DeadScript.dead = true;

        }
    }



}
