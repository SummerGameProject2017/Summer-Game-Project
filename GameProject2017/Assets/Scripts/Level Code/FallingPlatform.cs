using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool Shake = true;
    bool isFalling = false;
    bool HasFallen = false;
    float TimeStood = 0.0f;

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (isFalling)
        {
            TimeStood += Time.deltaTime;
            if (TimeStood >= 0.75f && TimeStood <= 1.49f)
            {
                if (Shake == true && HasFallen == false)
                {
                    transform.Translate(0.0f, 0.2f, 0.0f);
                    Shake = false;
                }
                else if (Shake == false && HasFallen == false)
                {
                    transform.Translate(0.0f, -0.2f, 0.0f);
                    Shake = true;
                }
            }
            else if (TimeStood > 1.5f)
            {
                HasFallen = true;
                GetComponent<Rigidbody>().useGravity = true;
            }
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isFalling = true;
        }
    }
    
    void OnTriggerExit(Collider col)
    {
        TimeStood = 0.0f;
        isFalling = false;
    }
    
}
