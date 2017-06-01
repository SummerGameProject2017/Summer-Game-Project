using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public PlayerController PlayerScript;
    bool dead = false;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(PlayerScript.health <= 0)
        {
            DEAD();
        }
	}

    void DEAD()
    {
        
        if (dead == false)
        {
            Instantiate(Resources.Load("DeathScreen"));
            dead = true;
        }
        
    }
}
