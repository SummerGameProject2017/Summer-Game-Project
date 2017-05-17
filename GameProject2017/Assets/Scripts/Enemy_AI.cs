using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum States { Patrol, Chase, Attack}; 
public class Enemy_AI : MonoBehaviour {

    public States aiState = States.Patrol;


    public bool attacking = true;
    public UnityEngine.AI.NavMeshAgent agent;
    public bool patrolling = true;
    float minPositionX;
    float maxPositionX;
    float minPositionZ;
    float maxPositionZ;
    bool playerRan = false;
    public GameObject target;
    private Transform player;
 //   public Animator anim;
    public GameObject patrolPoint;


    // Use this for initialization
    void Start () {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
  //      anim = GetComponent<Animator>();    //animate the enemies
        patrolPoint = Instantiate(target) as GameObject;
        minPositionX = this.transform.position.x - 10;
        maxPositionX = this.transform.position.x + 10;
        minPositionZ = this.transform.position.z - 10;
        maxPositionZ = this.transform.position.z + 10;
        patrolPoint.transform.position = new Vector3(Random.Range(minPositionX, maxPositionX), transform.position.y ,
                Random.Range(minPositionZ, maxPositionZ));
        agent.SetDestination(patrolPoint.transform.position);
    }

    // Update is called once per frame
    void Update () {

        player = GameObject.FindWithTag("Player").transform;


        Ray downwardsRay = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(downwardsRay, out hit, 1.0f))       
        {
            if (hit.collider.tag == "Environment")
                transform.position = hit.point; 

        }


        float offset = Vector3.Distance(player.transform.position, transform.position);
        if (offset <= 10 && offset > 3)
        {
            aiState = States.Chase;
        }
        if (offset <= 3)
        {
            aiState = States.Attack;
        }
        if (offset > 10)
        {
            aiState = States.Patrol;
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
        }
    

	}

    void Patrol()
    {
        agent.speed = 4;
        agent.SetDestination(patrolPoint.transform.position);
        
    }
    void Chase()
    {
        agent.speed = 7;
        agent.SetDestination(player.transform.position);
    }
    void Attack()
    {
        Debug.Log("Attacking");
    }
    IEnumerator OnTriggerEnter(Collider other)
    {
            
           
            patrolPoint.transform.position = new Vector3(transform.position.x, -500, transform.position.z);

   
            yield return new WaitForSeconds(2);
            patrolPoint.transform.position = new Vector3(Random.Range(minPositionX, maxPositionX), transform.position.y,
                Random.Range(minPositionZ, maxPositionZ));

        
        


    }

}
