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
    Quaternion lastRotation;

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

        Vector3 forward = GameObject.Find("PlayerCamera").transform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;
        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        float h = InputManager.GetAxis("Horizontal");
        float v = InputManager.GetAxis("Vertical");


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
            moveVector = lastMove;
        }


        

       


        if (InputManager.GetButtonDown("Jump") && jump >= 1 && isTalking == false)
        {
            jump--;
            verticalVelocity = jumpForce;
            isGrounded = false;

        }

       

       
        
            

        
        moveVector = (speed *( h * right + v * forward));


        moveAnim.x = InputManager.GetAxis("Horizontal"); //* speed;
        moveAnim.z = InputManager.GetAxis("Vertical");

        if (h == 0 && v == 0)
        {
            transform.rotation = lastRotation;
        }
        else
            transform.rotation = Quaternion.LookRotation(moveVector);




        moveVector.y = 0;
      
        moveVector.y = verticalVelocity;

     

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

        lastRotation = this.transform.rotation;


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
           
            yield return new WaitForSeconds(1);
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
