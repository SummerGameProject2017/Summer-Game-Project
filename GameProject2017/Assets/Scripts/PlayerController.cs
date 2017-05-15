using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 1;
    int jump = 2;
    public Vector3 moveVector;
    Vector3 lastMove;
    float jumpForce = 8;
    float gravity = 25;
    float verticalVelocity;
    CharacterController controller;
    private Conveyor otherscript;
    public GameObject[] belt;
    public Vector3 pushVector;
    public bool OnBelt;
    public bool OffBelt;
    int i;

    // Use this for initialization
    void Start ()
    {

        controller = GetComponent<CharacterController>();
        for (i = 0; i < belt.Length; i++)
        {
            otherscript = belt[i].GetComponent<Conveyor>();
            //pushVector = otherscript.pushVector;
        }
        if (i == null)
        {
            pushVector = new Vector3(0, 0, 0);
        }
        moveVector.x = 0;
        moveVector.y = 0;
        moveVector.z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (i > 0)
        {
            OnBelt = otherscript.OnBelt;
        }

        if (OnBelt == true)
        {
            //pushVector.x = otherscript.pushVector.x;
            //pushVector.y = otherscript.pushVector.y;
            //pushVector.z = otherscript.pushVector.z;
            pushVector = otherscript.pushVector;
            OffBelt = false;

        }
        if (OnBelt == false)
        {
            //pushVector.x = otherscript.pushVector.x;
            //pushVector.y = otherscript.pushVector.y;
            pushVector.x = 0;
            pushVector.y = 0;
            pushVector.z = 0;
            

        }
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



        moveVector.x = moveVector.x + pushVector.x;
        moveVector.z = moveVector.z + pushVector.z;
        moveVector.y = moveVector.y + pushVector.y;
        //moveVector.Normalize();
        moveVector *= moveSpeed;
        moveVector.y = verticalVelocity;

        controller.Move(moveVector * Time.deltaTime);
        lastMove = moveVector;



    }
    
    
}
