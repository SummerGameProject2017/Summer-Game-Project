using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Range(3.0f, 8.0f)]
    public float speed;

    Vector3 additionalmovement;

    public int jump = 2;    
    Vector3 moveVector;
    public Vector3 lastMove;
    public float jumpForce;
    float gravity = 25;
    public float verticalVelocity;
    public bool isTalking = false;
    CharacterController controller;
    public int health;
    public Vector3 moveAnim; // animation movement vector
    public Quaternion lastRotation;
    public GameObject hitpoint1;
    public GameObject hitpoint2;
    public GameObject hitpoint3;
    public bool attackMode = false;
    public bool newGame = true;
    public float h;
    public float v;

    Animation Anim;



    public bool bounceOnDog = false;
    public GameObject enemy;
    Health healthScript;

    // Use this for initialization
    void Start()
    {
        
     //   SaveLoad.Load();
     healthScript = GameObject.Find("HealthBar").GetComponent<Health>();
        controller = GetComponent<CharacterController>();
        health = 3;
        //    collectable = GetComponent<Gear>();
        if (newGame == true)
        {
            transform.localPosition = new Vector3(124.0f,-93.0f,-247.7f);
        }
        SaveLoad.Save();
    }

    // Update is called once per frame
    void Update()
    {
       
        

        Vector3 forward = GameObject.Find("PlayerCamera").transform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;
        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        h = InputManager.GetAxis("Horizontal");
        v = InputManager.GetAxis("Vertical");


        if (h > 0.65 || h < -0.65 || v > 0.65 || v < -0.65)
        {
            speed = 10;
        }
        else
        {
            speed = 5;
        }
       
        if (Player.Instance.lives > 0)
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

            
            
            // Jumping
            if (InputManager.GetButtonDown("Jump") && jump >= 1 && isTalking == false)
            {
                jump--;
                verticalVelocity = jumpForce;

            }


            // Pick up speed when joystick is moved more to one side
            if ((h > 0.65 || h < -0.65) && (v > 0.65 || v < -0.65))
            {
                speed = 7.5f;
                moveVector = (speed * (h * right + v * forward));
            }


            else
            {
                moveVector = (speed * (h * right + v * forward));
            }

            


            moveAnim.x = InputManager.GetAxis("Horizontal"); 
            moveAnim.z = InputManager.GetAxis("Vertical");

       
             if (attackMode == true && h == 0 && v == 0)
              {
                Vector3 direction;
                direction = (enemy.transform.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3);
              }
              else if (h == 0 && v == 0 && attackMode == false)
              {
                Vector3 direction;
                direction = (GameObject.Find("PlayerCamera").transform.position - transform.position).normalized;
                direction.y = 0;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3);
              }

              else
              {
                  transform.rotation = Quaternion.LookRotation(moveVector);
              } 
  


            moveVector += additionalmovement;

            // Clear
            additionalmovement = Vector3.zero;

            if (bounceOnDog == true)
            {
                verticalVelocity = jumpForce;
                bounceOnDog = false;
            }


            moveVector.y = verticalVelocity;



            controller.Move(moveVector * Time.deltaTime);
            lastMove = moveVector;

           
            lastRotation = this.transform.rotation;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectible")
        {
            CollectedGear();
        }
        if (other.gameObject.tag == "Fountain")
        {
            Player.Instance.lives = Player.Instance.maxLives;
            healthScript.HealthChange();
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

    //When health is added or subtracted this is called to display current healt
   

    public void CollectedGear()
    {
        GameObject Gear = (GameObject)Instantiate(Resources.Load("PlayerGear"), gameObject.transform.position + gameObject.transform.up * 3, gameObject.transform.rotation);

    }

    

}
