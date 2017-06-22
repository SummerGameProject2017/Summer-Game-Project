using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireRolling : MonoBehaviour {

    float Timer = 0.0f;
    public float RespawnTime;
    public Transform Tire;
    public float startTime;
    Vector3 TireStartPos;
    Quaternion TireStartRot;
    public bool canHitPlayer = true;
    Rigidbody rb;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        TireStartPos = transform.position;
        TireStartRot = transform.rotation;
        rb.useGravity = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        Timer += Time.deltaTime;
        if (Timer >= startTime)
        {
            GetComponent<MeshCollider>().enabled = true;
            rb.useGravity = true;
        }
        if (Timer >= RespawnTime + startTime)
        {
            if (Player.Instance.lives > 0)
            {
                canHitPlayer = true;
            }
            GetComponent<MeshCollider>().enabled = false;
            rb.useGravity = false;
            rb.ResetInertiaTensor();
            transform.position = TireStartPos;
            transform.rotation = TireStartRot;
            rb.velocity = new Vector3(0f, 0f, 0f);
            rb.angularVelocity = new Vector3(0f, 0f, 0f);
            Timer = startTime;
        }

        if (Player.Instance.lives <= 0)
        {
            canHitPlayer = false;
        }
       
            }
}
