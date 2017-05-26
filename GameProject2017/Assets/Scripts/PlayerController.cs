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
    int health;
    float hideplayerinfo;
    public GameObject[] Healthpoints;
    Gear collectable;
    public GameObject PlayerGear;
    
    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        health = 3;
        collectable = GetComponent<Gear>();
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

        Vector3 rotationVector = Vector3.zero;

        rotationVector.z = moveVector.z = InputManager.GetAxis("Vertical"); //* speed;
        rotationVector.x = moveVector.x = InputManager.GetAxis("Horizontal"); //* speed;

       

        transform.rotation = Quaternion.LookRotation(rotationVector);



        // if ((Input.GetKeyDown(JPGameManager.GM.jump) || Input.GetKeyDown(JPGameManager.GM.joyJump)) && jump >= 1)
        if (InputManager.GetButtonDown("Jump") && jump >= 1 && isTalking == false)
        {
            jump--;
            verticalVelocity = jumpForce;
            health -= 1;
            HealthChange();
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
        hideplayerinfo += Time.deltaTime;

        //hides health after 3 seconds
        if (hideplayerinfo > 3)
        {
            Healthpoints[2].SetActive(false);
            Healthpoints[1].SetActive(false);
            Healthpoints[0].SetActive(false);
            PlayerGear.SetActive(false);
        }
        //does the meme for collecting a gear
        if (collectable.collected == true)
        {
            CollectedGear();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectible")
        {
            CollectedGear();
        }
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

    //When health is added or subtracted this is called to display current health
    public void HealthChange()
    {
        hideplayerinfo = 0;
        if (health == 3)
        {
            Healthpoints[2].SetActive(true);
            Healthpoints[1].SetActive(true);
            Healthpoints[0].SetActive(true);

        }
        if(health == 2)
        {
            Healthpoints[1].SetActive(true);
            Healthpoints[0].SetActive(true);
        }
        if(health == 1)
        {
            Healthpoints[0].SetActive(true);
        }
     
        
    }

    public void CollectedGear()
    {
        hideplayerinfo = 0;
        PlayerGear.SetActive(true);
    }

}
