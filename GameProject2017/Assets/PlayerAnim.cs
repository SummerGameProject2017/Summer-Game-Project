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
    public Animator Anim;
    public bool attacking = false;
    PlayerController PC;
    GameOver DeadScript;
    // Use this for initialization
    void Start () {
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

        Anim.SetFloat("inputV", PC.moveAnim.z); // Set Vertical Movement
        Anim.SetFloat("inputH", PC.moveAnim.x); // Set Horizontal Movement

        if (Player.Instance.lives > 0)
        {
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


    }


    private IEnumerator OnTriggerEnter(Collider other)
    {
        //if the player falls in water play the falling in water animation then reset player to last save position
        if (other.gameObject.tag == "Water")
        {
            Anim.Play("Death-Water", -1, 0);
            yield return new WaitForSeconds(1);
            DeadScript.dead = true;
           
  
            

        }
    }



}
