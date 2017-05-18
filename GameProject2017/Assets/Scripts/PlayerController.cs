using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Range(3.0f, 10.0f)]
    public float speed = 5.0f;


    Vector3 additionalmovement;


    int jump = 2;
    Vector3 moveVector;
    Vector3 lastMove;
    float jumpForce = 10;
    float gravity = 25;
    float verticalVelocity;
    public bool isTalking = false;
    CharacterController controller;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(controller.isGrounded);

        if (controller.isGrounded)
        {
            verticalVelocity = -1;
            jump = 2;

        }

        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
            //verticalVelocity += Physics.gravity.y * Time.deltaTime;
            moveVector = lastMove;
        }

        //moveVector = Vector3.zero;
        //if (Input.GetKey(JPGameManager.GM.forward) || Input.GetAxis("Vertical") > 0.5)
        //{
        //    moveVector.x = 5;
        //}
        //if (Input.GetKey(JPGameManager.GM.backward) || Input.GetAxis("Vertical") < -0.5)
        //{
        //    moveVector.x = -5;
        //}
        //if (Input.GetKey(JPGameManager.GM.left) || Input.GetAxis("Horizontal") < -0.5)
        //{
        //    moveVector.z = 5;
        //}
        //if (Input.GetKey(JPGameManager.GM.right) || Input.GetAxis("Horizontal") > 0.5)
        //{
        //    moveVector.z = -5;
        //}

        moveVector.z = InputManager.GetAxis("Vertical"); //* speed;
        moveVector.x = InputManager.GetAxis("Horizontal"); //* speed;

        // if ((Input.GetKeyDown(JPGameManager.GM.jump) || Input.GetKeyDown(JPGameManager.GM.joyJump)) && jump >= 1)
        if (InputManager.GetButtonDown("Jump") && jump >= 1 && isTalking == false)
        {
            jump--;
            verticalVelocity = jumpForce;
        }

        //if (Input.GetButtonDown("Attack") || Input.GetKeyDown(JPGameManager.GM.joyAttack))
        //{
        //    Debug.Log("Attack");

        //    //attack code here;
        //}




        moveVector.y = 0;
        //moveVector.Normalize();
        moveVector *= speed;
        moveVector.y = verticalVelocity;

        // Add Aditional movement from level
        moveVector += additionalmovement;

        // Clear
        additionalmovement = Vector3.zero;

        controller.Move(moveVector * Time.deltaTime);
        lastMove = moveVector;



    }


    //
    // Summary:
    //     ///
    //     Add a movement direction to the player
    //     ///
    //
    // Parameters:
    //   movement: Direction the player have to be moved
    public void AddMovement(Vector3 movement)
    {

        additionalmovement += movement;

    }


}
