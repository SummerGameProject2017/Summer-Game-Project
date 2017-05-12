using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{

    public Rigidbody rb;
    public float BeltVelocity;
    public float speed = 155f;
    bool on = true;
    Vector2 offset = new Vector2(0f, 0f);


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    void OnCollisionStay(Collision obj)
    {
        BeltVelocity = speed * Time.deltaTime;
        obj.rigidbody.velocity = BeltVelocity * transform.forward;
    }

    void Update()
    {
        offset += new Vector2(0, 0.1f) * Time.deltaTime;
        // renderer.material.SetTextureOffset("_MainTex", offset);
    }
}
