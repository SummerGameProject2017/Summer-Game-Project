using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    PlayerAnim PA;

    AudioSource chimes;
    public AudioClip chimeSound;
    public AudioClip dogExplosion;
    public GameObject jumpParticle;
    public GameObject gear;

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
    float rotationAmount;
    TireRolling tire;
    WreckingBall rollingBall;

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

    ChangeScene changeSceneScript;
    GameObject collectableCount;
    public bool showCollectable = false;

    void Start()
    {
        PA = GetComponent<PlayerAnim>();
        controller = GetComponent<CharacterController>();
        chimes = GetComponent<AudioSource>();
        changeSceneScript = GameObject.Find("SceneManager").GetComponent<ChangeScene>();
        collectableCount = GameObject.Find("GearCounter");
        //   SaveLoad.Load();
        healthScript = GameObject.Find("HealthBar").GetComponent<Health>();
        jumpParticle = GameObject.Find("Player jump");
        
        
       
        
        //    collectable = GetComponent<Gear>();
        if (newGame == true)
        {
            Debug.Log("new Game 2");
            SaveLoad.Save();
        }
            
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!chimes.isPlaying)
        {
            gear = null;
        }
 rotationAmount = transform.rotation.y;

        if (InputManager.GetButtonDown("Pause"))
        {
            if (!SceneManager.GetSceneByName("PauseScene").isLoaded)
            {
                StartCoroutine(changeSceneScript.PauseMenu());
                PA.enabled = false;
                this.enabled = false;
                
                Time.timeScale = 0;
            }

        }


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
            
                h = InputManager.GetAxis("Horizontal");
                v = InputManager.GetAxis("Vertical");


                

                // Change Speed 
               
             if (h > 0.3 || h < -0.3 || v > 0.3|| v < -0.3)
                {
                    speed = 10;
                }
                else
                {
                    speed = 5;
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

                    moveVector = (speed * (h * right + v * forward));
                          
            

                if (InputManager.GetButtonDown("Attack") && attacking == false)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                    attacking = true;
                    PA.Anim.SetBool("Attack", true);
                    PA.Anim.Play("Attack");
                }
            
           
            if (attacking == true && PA.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4
               && PA.Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))

            {
                if (rotationAmount < 0.1 || rotationAmount > 0.5)
                {

                    transform.Rotate(Vector3.down * (360 * (Time.deltaTime * 2)));
                }
            }
            else if (attacking == false)
            {
                if (attackMode == true && h == 0 && v == 0)
                {
                    Vector3 direction;
                    direction = (enemy.transform.position - transform.position).normalized;
                    direction.y = 0;
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
                }

                else if (h == 0 && v == 0 && attackMode == false)
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
            transform.position = Vector3.Lerp(transform.position, enemy.transform.position + enemy.transform.forward * 20, Time.deltaTime * 10);
        }
        if (enemy != null && enemy.GetComponent<Dog>().dogDead == true)
        {
            enemy.GetComponent<Dog>().dogDead = false;
                 chimes.PlayOneShot(dogExplosion, 1);
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tire")
        {
            tire = other.gameObject.GetComponent<TireRolling>();
            if (tire.canHitPlayer == true)
            {
                tire.canHitPlayer = false;

                PA.Anim.Play("GetHit", -1, 0);
                transform.LookAt(tire.transform.position);
                AddMovement(Vector3.right * 200);

                    Player.Instance.LoseLife();
                    healthScript.HealthChange();
                
                if (Player.Instance.lives <= 0)
                {
                    PA.Anim.Play("Death-Enemy", -1, 0);
                   
                        StartCoroutine(changeSceneScript.DisplayGameOverScreen("GameOver"));
                    
                }
           }
}
        if (other.gameObject.tag == "RollingBalls")
        {
            rollingBall = other.gameObject.GetComponent<WreckingBall>();

            if (rollingBall.canHitPlayer == true)
            {
                rollingBall.canHitPlayer = false;
                PA.Anim.Play("GetHit", -1, 0);


           //     transform.position = Vector3.Lerp(transform.position, other.transform.forward * 10, Time.deltaTime * 2);

                AddMovement(new Vector3(200, 0, 10f));
                

                Player.Instance.LoseLife();
                healthScript.HealthChange();

                if (Player.Instance.lives <= 0)
                {
                    PA.Anim.Play("Death-Enemy", -1, 0);

                    StartCoroutine(changeSceneScript.DisplayGameOverScreen("GameOver"));

                }
            }

        }
       if (other.gameObject.tag == "Collectible")
            {
            gear = other.gameObject;

            if (gear.GetComponent<Gear>().collected == true)
            {
                gear.GetComponent<Gear>().collected = false;
                chimes.PlayOneShot(chimeSound, 1);
            }

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
