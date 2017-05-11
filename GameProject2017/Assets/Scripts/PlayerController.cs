﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 5;


    int jump = 2;
    Vector3 moveVector;
    Vector3 lastMove;
    float jumpForce = 8;
    float gravity = 25;
    float verticalVelocity;
    CharacterController controller;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
	}

    // Update is called once per frame
    void Update() {

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
		if (Input.GetKey(JPGameManager.GM.forward))
        {
            moveVector.x = 5;
        }
         if (Input.GetKey(JPGameManager.GM.backward))
         {
            moveVector.x = -5;
        }
         if (Input.GetKey(JPGameManager.GM.left))
         {
            moveVector.z = 5;
        }
         if (Input.GetKey(JPGameManager.GM.right))
         {
            moveVector.z = -5;
        }

        if ((Input.GetKeyDown(JPGameManager.GM.jump) || Input.GetKeyDown(JPGameManager.GM.joyJump)) && jump >= 1)
        {
            jump--;
            verticalVelocity = jumpForce;
        }

        moveVector.y = 0;
        moveVector.Normalize();
        moveVector *= moveSpeed;
        moveVector.y = verticalVelocity;

        controller.Move(moveVector * Time.deltaTime);
        lastMove = moveVector;
    }
    
    
}
