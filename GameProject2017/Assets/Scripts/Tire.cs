using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tire : MonoBehaviour
{
    public PlayerController PlayerScript;
    public float blastforce;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Player")
        {
        
            Blast();
            
        }
    }

    private void Blast()
    {
        PlayerScript.verticalVelocity += blastforce;
    }

}
