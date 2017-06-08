using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool Shake = true;
    bool isFalling = false;
    bool HasFallen = false;
    float TimeStood = 0.0f;
    float TimeFallen = 0.0f;
    public GameObject Dust;
    public GameObject Platform;
    Vector3 Startpos;

    // Use this for initialization
    void Start ()
    {
        Startpos = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isFalling)
        {
            TimeStood += Time.deltaTime;
            //Make platform shake
            if (TimeStood >= 0.5f && TimeStood <= 0.99f)
            {
                if (Shake == true && HasFallen == false)
                {
                //    transform.Translate(0.0f, 0.2f, 0.0f);
                    GameObject DustParticle = Instantiate(Dust, transform.position, transform.rotation) as GameObject;
                    Destroy(DustParticle, 2.6f); //Deletes the particles after 2.6 seconds
                    Shake = false;
                }
                else if (Shake == false && HasFallen == false)
                {
                //    transform.Translate(0.0f, -0.2f, 0.0f);
                    Shake = true;
                }
            }
            //Make platform fall after being stood on for 1.5 seconds
            else if (TimeStood > 1.0f)
            {
                HasFallen = true;
                GetComponent<Rigidbody>().useGravity = true;
            }
        }
        else if (HasFallen == true)
        {
            TimeFallen += Time.deltaTime;
        }
        //Make platform respawn after 10 seconds
        if (HasFallen == true && TimeFallen >= 10)
        {
            GetComponent<Rigidbody>().useGravity = false;
            Instantiate(Platform, Startpos, transform.rotation);
            Destroy(Platform);
            HasFallen = false;
            TimeFallen = 0.0f;
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
