using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States { Patrol, Chase, Attack, Idle, Dead, Stunned };
public class Dog : Enemy
{

    public States aiState = States.Patrol;


    public bool attacking = true;
    public UnityEngine.AI.NavMeshAgent agent;
    public bool patrolling = true;
    float XPosition;
    float ZPosition;

    float minPositionX;
    float maxPositionX;
    float minPositionZ;
    float maxPositionZ;
    public float maxDistanceX = 0;
    public float maxDistanceZ = 0;
    public float minDistanceX = 0;
    public float minDistanceZ = 0;
    
    public GameObject target;
    private Transform player;
    public Animator anim;
    GameObject patrolPoint;
    bool idle = false;
    bool firstAttack = true;
    short X = 0;
    short Z = 0;
    public short health = 2;
    PlayerController playerScript;
    PlayerAnim animationScript;
    public bool aiStunned = false;
    bool canBeHit = false;
    public GameObject DogExplosionParticle;
    public GameOver DeadScript;

    public override void OnStart()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        animationScript = GameObject.FindWithTag("Player").GetComponent<PlayerAnim>();
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();    //animate the enemies
        patrolPoint = Instantiate(target) as GameObject;

        X = (short)Random.Range(0, 2);
        Z = (short)Random.Range(0, 2);
        minPositionX = this.transform.position.x - minDistanceX;
        maxPositionX = this.transform.position.x + maxDistanceX;
        minPositionZ = this.transform.position.z - minDistanceZ;
        maxPositionZ = this.transform.position.z + maxDistanceZ;
        //set the bounds the ai will be moving around

        //depending on a random number between 0 and 1 set the point for the ai to move to
        if (X == 0)
        {
            XPosition = minPositionX;
        }
        else
        {
            XPosition = maxPositionX;
        }
        if (Z == 0)
        {
            ZPosition = minPositionZ;
        }
        else
        {
            ZPosition = maxPositionZ;
        }
    
        
        patrolPoint.transform.position = new Vector3(XPosition, transform.position.y, ZPosition);
        //set the patrol point gameobject in the scene based on the random position
        agent.SetDestination(patrolPoint.transform.position);
        //tell the ai to go to the patrol point
    }


    public override void OnUpdate()
    {
        player = GameObject.FindWithTag("Player").transform;


        Ray downwardsRay = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(downwardsRay, out hit, 1.0f))
        {
            if (hit.collider.tag == "Environment")
                transform.position = hit.point;

        }
        // put the ai on the ground using raycasting

        
        float offset = Vector3.Distance(player.transform.position, transform.position);
        //find the distance between the player and the ai and change state based on health of ai and distance
        
      

       
        if (health > 0 && aiStunned == false)
        {
                if (offset <= 15 && offset > 3.9)
                {
                    idle = false;
                    aiState = States.Chase;
                }
                if (offset <= 3.9 && (player.transform.position.y - 1.25) <= transform.position.y && animationScript.attacking == false)
                {
                    aiState = States.Attack;
                }
                if (offset <= 3.9 && (player.transform.position.y - 1.25) <= transform.position.y && animationScript.attacking == true)
                {
                agent.SetDestination(transform.position);
                firstAttack = false;
                aiState = States.Attack;
                }
                if (offset > 15 && idle == false)
                {
                    aiState = States.Patrol;
                }
                if (idle == true)
                {
                    aiState = States.Idle;
                }

        }



        
        //choose the state and which method to call based on the state of tha ai
        switch (aiState)
        {
            case States.Patrol:
                Patrol();
                break;
            case States.Chase:
                Chase();
                break;
            case States.Attack:
                Attack();
                break;
            case States.Idle:
                Idle();
                break;
            case States.Dead:
                StartCoroutine(EnemyDead());
                break;
            case States.Stunned:
                break;
        }

        //change this to whatever attack button is
        

        if (animationScript.Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && animationScript.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.2)
        {
            canBeHit = true;
        }
        if (animationScript.Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && animationScript.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime> 0.8)
        {
            canBeHit = false;
        }


    }
    //walk to diffenent patrol points
    void Patrol()
    {
        patrolPoint.SetActive(true);
        agent.speed = 5;
        anim.SetBool("Walk", true);
        anim.SetBool("Run", false);
        anim.SetBool("Bite", false);
        agent.SetDestination(patrolPoint.transform.position);

    }
    //chase after the player
    void Chase()
    {
        firstAttack = true;
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        patrolPoint.SetActive(false);
        agent.speed = 7;
        anim.SetBool("Walk", false);
        anim.SetBool("Run", true);
        anim.SetBool("Bite", false);
        agent.SetDestination(player.transform.position);
    }
    //attack the player and call the player loose life method
    void Attack()
    {
        agent.SetDestination(transform.position);
        if (firstAttack == true)
        {
            //for the first attack reset the animation timer and call the attack animation
            anim.Play("Bite", -1, 0);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            anim.SetBool("Bite", true);
            animationScript.Anim.Play("GetHit", -1, 0);
//            playerScript.lastRotation = Quaternion.LookRotation(this.transform.position);
            Player.Instance.LoseLife();
            playerScript.HealthChange();
            firstAttack = false;

            if (Player.Instance.lives <= 0)
            {
                animationScript.Anim.Play("Death-Enemy", -1, 0);
                DeadScript.dead = true;
            }

        }
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        
       if (anim.GetCurrentAnimatorStateInfo(0).IsName("Combat Idle") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 2)
        {
            //if the attack idle animation reaches 2 seconds in loop length change to attack animation and reset the timer. 
            animationScript.Anim.Play("GetHit", -1, 0);
            Player.Instance.LoseLife();
            anim.Play("Bite", -1, 0);
            anim.SetBool("Idle", false);
            playerScript.HealthChange();

            if (Player.Instance.lives <= 0)
            {
                animationScript.Anim.Play("Death-Enemy", -1, 0);
                DeadScript.dead = true;
            }

        }
       else
        {
            //rotate for bad angle in idle animation
            transform.Rotate(3, 0, 0);
            anim.SetBool("Idle", true);
            anim.SetBool("Bite", false);
        }


        
       
    }
    //when the ai reaches a patrol point change to idle animation
    void Idle()
    {
        transform.Rotate(3, 0, 0);
        agent.speed = 0;
        agent.SetDestination(transform.position);
        anim.SetBool("Walk", false);
        anim.SetBool("Run", false);
        anim.SetBool("Bite", false);
    }

    
    
    IEnumerator OnTriggerStay(Collider other)
    {
        //when the player reaches a patrol point, deactivate the patrol point and reactivate at a new random position
        if (other.gameObject.tag == "PatrolPoint")
        {
            idle = true;
            patrolPoint.SetActive(false);
            yield return new WaitForSeconds(3);
            patrolPoint.SetActive(true);
            X = (short)Random.Range(0, 2);
            Z = (short)Random.Range(0, 2);
            if (X == 0)
            {
                XPosition = minPositionX;
            }
            else
            {
                XPosition = maxPositionX;
            }
            if (Z == 0)
            {
                ZPosition = minPositionZ;
            }
            else
            {
                ZPosition = maxPositionZ;
            }
            patrolPoint.transform.position = new Vector3(XPosition, transform.position.y, ZPosition);
            idle = false;
        }

       
    }



    private void OnTriggerEnter(Collider other)
    {
        //if the player jumps on the ai chnge to stunned state and bounce the player
         if (other.gameObject.tag == "Player" && aiStunned == false && health > 0)
        {
            playerScript.verticalVelocity = playerScript.jumpForce;
            aiStunned = true;
            aiState = States.Stunned;
            StartCoroutine(DogStunned());
        }
        //damage the robot
        if (other.gameObject.name == "Hammer" && canBeHit == true)
        {
            if (health > 1)
            {
                health--;
                StartCoroutine(DogHit());
                canBeHit = false;
                
        }
        else
        {
                GetComponent<Collider>().enabled = false;
                health--;
                canBeHit = false;
                aiState = States.Dead;
            }


        }
    }
   
    //when the ai is out of health change to dead animation and after 3 seconds destroy the ai gameobject
    IEnumerator EnemyDead()
    {
        canBeHit = false;
        agent.speed = 0;
        anim.SetBool("Sunned", false);
        anim.SetBool("Dead", true);
        anim.SetBool("Walk", false);
        anim.SetBool("Run", false);
        anim.SetBool("Bite", false);
        anim.SetBool("Idle", false);
        
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
        Instantiate(DogExplosionParticle, transform.position, Quaternion.identity);
    }

    
    //play the stunned animation for 3 seconds then change states
    IEnumerator DogStunned()
    {
        
            agent.SetDestination(transform.position);
            agent.updateRotation = false;

            anim.SetBool("Stunned", true);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            anim.SetBool("Bite", false);
            anim.SetBool("Idle", false);
            yield return new WaitForSeconds(3);
            anim.SetBool("Stunned", false);
            agent.updateRotation = true;
            firstAttack = true;
            aiStunned = false;
                
    }

    IEnumerator DogHit()
    {
        anim.SetBool("Stunned", false);
        agent.SetDestination(transform.position);
        agent.updateRotation = false;
        anim.Play("Hit", -1, 0);
        anim.SetBool("Walk", false);
        anim.SetBool("Run", false);
        anim.SetBool("Bite", false);
        anim.SetBool("Idle", false);
        yield return new WaitForSeconds(1);

        agent.updateRotation = true;
    }

}
