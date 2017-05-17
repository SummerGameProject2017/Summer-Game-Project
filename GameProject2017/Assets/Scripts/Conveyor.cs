using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{

    
    public Rigidbody rb;
    public float BeltVelocity;
    public Vector3 speedVector;
    public bool OnBelt;
    Animator anim;
    public PlayerController PlayerScript;
    public Rigidbody playerbody;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        OnBelt = false;
        anim = GetComponent<Animator>();
        
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Player")
        {
            OnBelt = true;   
        }
    }

    private void OnTriggerExit(Collider obj)
    {
        if (obj.tag == "Player")
        {
            OnBelt = false;
        }
        return;
    }


    void Update()
    {
        anim.SetFloat("Speed", BeltVelocity);
        if(OnBelt == true)
        {
           // PlayerScript.playerbody.transform.position += BeltVelocity * speedVector / 10;
            //Debug.Log("Belt Move" + BeltVelocity + ' ' + PlayerScript.playerbody.transform.name);
        }
    }

    
}
