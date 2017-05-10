using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class belt : MonoBehaviour
{

    public Rigidbody rb;
    float speed = 155f;
    bool on = true;
    Vector2 offset = new Vector2(0f, 0f);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    void OnCollisionStay(Collision obj)
    {
        float beltVelocity = speed * Time.deltaTime;
        obj.rigidbody.velocity = beltVelocity * transform.forward;
    }

    void Update()
    {
        offset += new Vector2(0, 0.1f) * Time.deltaTime;
        // renderer.material.SetTextureOffset("_MainTex", offset);
    }
}

