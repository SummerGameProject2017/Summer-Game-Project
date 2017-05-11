using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 5;
    public Rigidbody rb;
    bool onGround = false;
    int jump = 2;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update() {

        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down * 0.5f, Color.red, 1);   //change last variable to adjust for height of player later
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.5f))    //change last variable to adjust for height of player later
        {
            if (hit.transform.gameObject.tag != "Player")
            {
                jump = 2;
            }
        }
        else
        {
            onGround = false;
        }
        

		if (Input.GetKey(JPGameManager.GM.forward))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(JPGameManager.GM.backward))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(JPGameManager.GM.left))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(JPGameManager.GM.right))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(JPGameManager.GM.jump) && jump >= 1)
        {
            jump--;
            rb.AddForce(Vector3.up*200);
        }

    }
}
