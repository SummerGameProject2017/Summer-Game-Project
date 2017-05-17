using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //private readonly float DEADZONE = 0.5f;


    private Rigidbody playerbody;
    private CharacterController controller;


    //int jump = 2;
    private Vector3 moveVector;
    private Vector3 additionalmovement;
    //private Vector3 lastMove;
    float jumpForce = 8;
    float gravity = 25;
    float verticalVelocity;


    [Range(1.0f, 10.0f)]
    public float speed = 1.0f;

    public Conveyor Belt;
    public Vector3 speedVector;
    public bool OnBelt;
   

    // Use this for initialization
    void Start ()
    {

        controller = GetComponent<CharacterController>();
        playerbody = GetComponent<Rigidbody>();
        moveVector = new Vector3(0, 0, 0);
        Belt = GetComponent<Conveyor>();
        //InputManager.DisableBytime(2);

    }

    // Update is called once per frame
    void Update()
    {
        //if (controller.isGrounded)
        //{
        //    verticalVelocity = -1;
        //    jump = 2; 
        //}

        //else
        //{
        //    verticalVelocity -= gravity * Time.deltaTime;
        //    moveVector = lastMove;
        //}





        //moveVector = Vector3.zero;

        //if (Input.GetKey(JPGameManager.GM.forward) || Input.GetAxis("Vertical") > 0.5)
        //{
        //    moveVector.x += 5;
        //}
        //if (Input.GetKey(JPGameManager.GM.backward) || Input.GetAxis("Vertical") < -0.5)
        //{
        //    moveVector.x += -5;
        //}
        //if (Input.GetKey(JPGameManager.GM.left) || Input.GetAxis("Horizontal") < -0.5)
        //{
        //    moveVector.z += 5;
        //}
        //if (Input.GetKey(JPGameManager.GM.right) || Input.GetAxis("Horizontal") > 0.5)
        //{
        //    moveVector.z += -5;
        //}

        //if ((Input.GetKeyDown(JPGameManager.GM.jump) || Input.GetKeyDown(JPGameManager.GM.joyJump)) && jump >= 1)
        //{
        //    jump--;
        //    verticalVelocity = jumpForce;
        //}

        //if (Input.GetKeyDown(JPGameManager.GM.attack) || Input.GetKeyDown(JPGameManager.GM.joyAttack))
        //{
        //    Debug.Log("Attack");

        //    //attack code here;
        //}

        // Read from Input
        moveVector.z = InputManager.GetAxis("Vertical") * speed;
        moveVector.x = InputManager.GetAxis("Horizontal") * speed;
       

        // Add Aditional movement from level
        moveVector += additionalmovement;

        // Clear
        additionalmovement = Vector3.zero;

        // Add Vertical Velocity
        moveVector.y = verticalVelocity;

        // Move player
        controller.Move(moveVector * Time.deltaTime);
        //lastMove = moveVector;

        if (OnBelt == true)
        {
             

        }
        AddMovement(speedVector);

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
        movement = speedVector;
    }

    
    
}
