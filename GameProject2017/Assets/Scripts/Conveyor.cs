using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{

    public Rigidbody rb;
    public float BeltVelocity;
    public Vector3 speedVector;
    Vector2 offset = new Vector2(0f, 0f);

    //under this comment are things used to interact with the player
    public Vector3 moveVector;
    public Vector3 pushVector;
    public bool OnBelt;
    public bool OffBelt;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
       // otherscript = Player.GetComponent<PlayerController>();
        OnBelt = false;
        OffBelt = false;
    }



    /* void OnCollisionStay(Collision obj)
      {

          pushVector = speedVector * Time.deltaTime;
          obj.rigidbody.velocity = BeltVelocity * transform.forward;
      }*/

    private void OnTriggerEnter(Collider obj)
    {
        //Debug.Log("on");
        if (obj.tag == "Player")
        {
            OnBelt = true;   
        }
        //Debug.Log("playeron");
    }

    private void OnTriggerExit(Collider obj)
    {
        //Debug.Log("off");
        if (obj.tag == "Player")
        {
            OnBelt = false;
           // Debug.Log("Playeroff");
        }
        return;
    }


    void Update()
    {
        
        offset += new Vector2(0, 0.1f) * Time.deltaTime;
        
        // renderer.material.SetTextureOffset("_MainTex", offset);
    }


}
