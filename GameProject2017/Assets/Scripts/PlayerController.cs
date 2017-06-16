using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    PlayerAnim PA;


    
    public GameObject jumpParticle;
    

    [Range(3.0f, 8.0f)]
    public float speed;
    float gravity = 25;
    public float jumpForce;
    public float verticalVelocity;    

    public int jump = 2;       
    
    Vector3 moveVector;
    Vector3 additionalmovement;
    public Vector3 moveAnim; // animation movement vector

    public bool isGrounded = true; //  player on the ground bool
    public bool attackMode = false;
    public bool newGame = true;
    public bool isTalking = false;
    public GameObject healingParticle;


   // [HideInInspector]
    public bool attacking = false;
    [HideInInspector]
    public Vector3 lastMove;
    [HideInInspector]
    public float h;
    [HideInInspector]
    public float v;
    [HideInInspector]
    public bool bounceOnDog = false;
    [HideInInspector]
    public GameObject enemy;
    Health healthScript;
    
    [HideInInspector]
    public bool fallBack = false;
    [HideInInspector]
    public bool destroyHealingParticle;
    [HideInInspector]
    public GameObject healingEffect;
    

    float waitTime = 0;
    bool moving  = false;


    GameObject collectableCount;
    public bool showCollectable = false;

    // Use this for initialization
    void Start()
    {
        collectableCount = GameObject.Find("GearCounter");
        //   SaveLoad.Load();
        healthScript = GameObject.Find("HealthBar").GetComponent<Health>();
        
        controller = GetComponent<CharacterController>();
        PA = GetComponent<PlayerAnim>();
       
        
        //    collectable = GetComponent<Gear>();
        if (newGame == true)
        {
            transform.localPosition = new Vector3(124.0f,-93.0f,-247.7f);
        }

      //  SaveLoad.Save();
    }

    // Update is called once per frame
    void Update()
    {
        if (showCollectable == true)
        {
            collectableCount.SetActive(true);
        }
        else
        {
            collectableCount.SetActive(false);
        }

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
                isGrounded = true;
               
            }
            else
            {
                verticalVelocity -= gravity * Time.deltaTime;
                moveVector = lastMove;
            }

            // Jump
            if (InputManager.GetButtonDown("Jump") && jump >= 1 && isTalking == false)
            {
                jump--;
                verticalVelocity = jumpForce;
                isGrounded = false;
                PA.Anim.Play("Jump");
                PA.Anim.SetBool("Attack", false);
                PA.Anim.SetBool("Jump", true);
                PA.Anim.SetBool("Landed", false);
                jumpParticle.transform.position = transform.position;
                jumpParticle.GetComponent<ParticleSystem>().Emit(25);


            }


            // Change Speed 
            if ((h > 0.65 || h < -0.65) && (v > 0.65 || v < -0.65))
            {
                speed = 7.5f;
                moveVector = (speed * (h * right + v * forward));
            }

            if (h != 0 || v != 0)
            {
                moving = true;
            }

            if (h == 0 && v == 0 && moving == true)
            {
                moving = false;
                waitTime = Time.time;
            }

            else
            {
                moveVector = (speed * (h * right + v * forward));
            }

            moveAnim.x = InputManager.GetAxis("Horizontal"); 
            moveAnim.z = InputManager.GetAxis("Vertical");


            if (InputManager.GetButtonDown("Attack"))
            {
                attacking = true;
                PA.Anim.SetBool("Attack", true);
                PA.Anim.Play("Attack");        
            }
       
             if (attacking == true && PA.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4 
                && PA.Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))

            {
                transform.Rotate(Vector3.down * 1080 * Time.deltaTime);
            }

            
            else if (attackMode == true && h == 0 && v == 0 && attacking == false)
              {
                Vector3 direction;
                direction = (enemy.transform.position - transform.position).normalized;
                direction.y = 0;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3);
              }
           
             else if (h == 0 && v == 0 && attackMode == false && attacking == false)
              {
                if (Time.time >= waitTime + 3 && PA.Anim.GetCurrentAnimatorStateInfo(0).IsTag("Movement"))
                {
                    Vector3 direction;
                    direction = (GameObject.Find("PlayerCamera").transform.position - transform.position).normalized;
                    direction.y = 0;
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1.5f);
                    
                }
              }
             
            else
            {
                transform.rotation = Quaternion.LookRotation(moveVector);
            }



            moveVector += additionalmovement;

            verticalVelocity += additionalmovement.y;

            // Clear
            additionalmovement = Vector3.zero;

            if (bounceOnDog == true)
            {
                jump = 1;
                verticalVelocity = jumpForce;
                bounceOnDog = false;
            }


            moveVector.y = verticalVelocity;



            controller.Move(moveVector * Time.deltaTime);
            lastMove = moveVector;
        }

        if (fallBack == true)
        {
            transform.position = Vector3.Lerp(transform.position, enemy.transform.position + enemy.transform.forward * 20, Time.deltaTime * 5);
        }
        
    }

   
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Fountain")
        {
            destroyHealingParticle = false;
            healthScript.HealthChange();
            Instantiate(healingParticle, transform.position, Quaternion.identity);
            Player.Instance.lives = Player.Instance.maxLives;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Fountain")
        {
            destroyHealingParticle = true;
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



}
