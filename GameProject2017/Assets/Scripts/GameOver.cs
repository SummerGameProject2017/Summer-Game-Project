using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameOver : MonoBehaviour
{
    public Player PlayerScript;
    public bool dead = false;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(dead == true)
        {
            DEAD();
        }
      
	}

    void DEAD()
    {
        
        
            Instantiate(Resources.Load("GAMEOVER"));
            dead = false;
        
    }

}
