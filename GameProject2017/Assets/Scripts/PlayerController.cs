﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Rigidbody playerbody;
    public float moveSpeed = 1;
    int jump = 2;
    public Vector3 moveVector;
    Vector3 lastMove;
    float jumpForce = 8;
    float gravity = 25;
    float verticalVelocity;
    CharacterController controller;

    // Use this for initialization
    void Start ()
    {

        controller = GetComponent<CharacterController>();
        playerbody = GetComponent<Rigidbody>();
        moveVector = new Vector3(0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -1;
            jump = 2; 
        }

        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
            moveVector = lastMove;
        }
        
        moveVector = Vector3.zero;
		if (Input.GetKey(JPGameManager.GM.forward) || Input.GetAxis("Vertical") > 0.5)
        {
            moveVector.x += 5;
        }
         if (Input.GetKey(JPGameManager.GM.backward) || Input.GetAxis("Vertical") < -0.5)
         {
            moveVector.x += -5;
        }
         if (Input.GetKey(JPGameManager.GM.left) || Input.GetAxis("Horizontal") < -0.5)
         {
            moveVector.z += 5;
        }
         if (Input.GetKey(JPGameManager.GM.right) || Input.GetAxis("Horizontal") > 0.5)
         {
            moveVector.z += -5;
        }

        if ((Input.GetKeyDown(JPGameManager.GM.jump) || Input.GetKeyDown(JPGameManager.GM.joyJump)) && jump >= 1)
        {
            jump--;
            verticalVelocity = jumpForce;
        }

        if (Input.GetKeyDown(JPGameManager.GM.attack) || Input.GetKeyDown(JPGameManager.GM.joyAttack))
        {
            Debug.Log("Attack");

            //attack code here;
        }




        moveVector += moveVector;
        moveVector *= moveSpeed;
        moveVector.y = verticalVelocity;

        controller.Move(moveVector * Time.deltaTime);
        lastMove = moveVector;



    }
    
    
}
