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
    
    public GameObject target;
    private Transform player;
    public Animator anim;
    GameObject patrolPoint;
    bool idle = false;
    bool firstAttack = true;
    int X = 0;
    int Z = 0;
    public int health = 1;
    PlayerController playerScript;
    public bool aiStunned = false;

    public override void OnStart()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();    //animate the enemies
        patrolPoint = Instantiate(target) as GameObject;

        X = Random.Range(0, 2);
        Z = Random.Range(0, 2);
        minPositionX = this.transform.position.x - 10;
        maxPositionX = this.transform.position.x + 10;
        minPositionZ = this.transform.position.z - 10;
        maxPositionZ = this.transform.position.z + 10;



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
        agent.SetDestination(patrolPoint.transform.position);
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

        
        float offset = Vector3.Distance(player.transform.position, transform.position);

        
      

        if (health <= 0)
        {
            aiState = States.Dead;
        }
        if (health > 0 && aiStunned == false)
        {
                if (offset <= 15 && offset > 4)
                {
                    idle = false;
                    aiState = States.Chase;
                }
                if (offset <= 4 && player.transform.position.y <= transform.position.y)
                {
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



       

    }
    void Patrol()
    {
        patrolPoint.SetActive(true);
        agent.speed = 2;
        anim.SetBool("Walk", true);
        anim.SetBool("Run", false);
        anim.SetBool("Bite", false);
        agent.SetDestination(patrolPoint.transform.position);

    }
    void Chase()
    {
        firstAttack = true;
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        patrolPoint.SetActive(false);
        agent.speed = 4;
        anim.SetBool("Walk", false);
        anim.SetBool("Run", true);
        anim.SetBool("Bite", false);
        agent.SetDestination(player.transform.position);
    }
    void Attack()
    {
        if (firstAttack == true)
        {
            anim.Play("Bite", -1, 0);
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            anim.SetBool("Bite", true);
            Player.Instance.LoseLife();
            firstAttack = false;
        }
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        
       if (anim.GetCurrentAnimatorStateInfo(0).IsName("Combat Idle") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 2)
        {
            Player.Instance.LoseLife();
            anim.Play("Bite", -1, 0);
            anim.SetBool("Idle", false);
        }
       else
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Bite", false);
        }


        
       
    }
    void Idle()
    {
        agent.speed = 0;
        agent.SetDestination(transform.position);
        anim.SetBool("Walk", false);
        anim.SetBool("Run", false);
        anim.SetBool("Bite", false);
    }

    
    IEnumerator OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PatrolPoint")
        {
            idle = true;
            patrolPoint.transform.position = new Vector3(transform.position.x, -500, transform.position.z);
            yield return new WaitForSeconds(3);
            X = Random.Range(0, 2);
            Z = Random.Range(0, 2);
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
         if (other.gameObject.tag == "Player" && aiStunned == false)
        {
            playerScript.verticalVelocity = playerScript.jumpForce;
            aiStunned = true;
            aiState = States.Stunned;
            StartCoroutine(DogStunned());
        }

         if (other.gameObject.name == "Hammer")
        {
            health--;
        }
    }
   

    IEnumerator EnemyDead()
    {
        agent.speed = 0;
        anim.SetBool("Dead", true);
        anim.SetBool("Walk", false);
        anim.SetBool("Run", false);
        anim.SetBool("Bite", false);
        anim.SetBool("Idle", false);
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

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

}
