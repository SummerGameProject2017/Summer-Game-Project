using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Range(3.0f, 10.0f)]
    public float speed = 5.0f;

    Vector3 additionalmovement;

    public int jump = 2;    
    Vector3 moveVector;
    Vector3 lastMove;
    public float jumpForce = 10;
    float gravity = 25;
    public float verticalVelocity;
    public bool isTalking = false;
    CharacterController controller;
    int health;
    public Vector3 moveAnim; // animation movement vector
    public bool isGrounded = true; //  player on the ground bool
    Vector3 rotationVector = Vector3.zero;


    // Use this for initialization
    void Start()
    {
        
        controller = GetComponent<CharacterController>();
        health = 3;
    //    collectable = GetComponent<Gear>();
    }

    // Update is called once per frame
    void Update()
    {

        if (controller.isGrounded)
        {
            verticalVelocity = -1;
            jump = 2;
            isGrounded = true;
            Debug.Log("Grounded");
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

        

        rotationVector.z = moveAnim.z = moveVector.z = InputManager.GetAxis("Vertical"); //* speed;
        rotationVector.x = moveAnim.x = moveVector.x = InputManager.GetAxis("Horizontal"); //* speed;



        transform.rotation = Quaternion.LookRotation(new Vector3(rotationVector.x, rotationVector.y, rotationVector.z));



        // if ((Input.GetKeyDown(JPGameManager.GM.jump) || Input.GetKeyDown(JPGameManager.GM.joyJump)) && jump >= 1)
        if (InputManager.GetButtonDown("Jump") && jump >= 1 && isTalking == false)
        {
            jump--;
            verticalVelocity = jumpForce;
            isGrounded = false;

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

        //hides health after 3 seconds
        /*        if (hideplayerinfo > 3)
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
                */



        
    }
    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectible")
        {
            CollectedGear();
        }
        //if the player falls in water play the falling in water animation then reset player to last save position
        if (other.name == "Water")
        {
           
            yield return new WaitForSeconds(1.5f);
            SaveLoad.Load();
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

        if (Player.Instance.lives == 3)
        {
            GameObject Hitpoint = (GameObject)Instantiate(Resources.Load("Hitpoint"), gameObject.transform.position + gameObject.transform.up * 2, gameObject.transform.rotation);
            Instantiate(Resources.Load("Hitpoint"), gameObject.transform.position + gameObject.transform.up * 2 - (gameObject.transform.right * 1), gameObject.transform.rotation);
            Instantiate(Resources.Load("Hitpoint"), gameObject.transform.position + gameObject.transform.up * 2 + (gameObject.transform.right * 1), gameObject.transform.rotation);

        }
        if (Player.Instance.lives == 2)
        {
            GameObject Hitpoint = (GameObject)Instantiate(Resources.Load("Hitpoint"), gameObject.transform.position + gameObject.transform.up * 2, gameObject.transform.rotation);
            Instantiate(Resources.Load("Hitpoint"), gameObject.transform.position + gameObject.transform.up * 2 - (gameObject.transform.right * 1), gameObject.transform.rotation);
        }
        if (Player.Instance.lives == 1)
        {
            GameObject Hitpoint = (GameObject)Instantiate(Resources.Load("Hitpoint"), gameObject.transform.position + gameObject.transform.up * 2, gameObject.transform.rotation);
        }


    }
    public void CollectedGear()
    {
        GameObject Gear = (GameObject)Instantiate(Resources.Load("PlayerGear"), gameObject.transform.position + gameObject.transform.up * 3, gameObject.transform.rotation);

    }

}
