using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameOver : MonoBehaviour
{
    public Player PlayerScript;
    public bool dead = false;
    private GameObject Death;
    // Use this for initialization
    void Start ()
    {
        Death = GameObject.Find("GAMEOVER");
        Death.SetActive(false);
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


        //Instantiate(Resources.Load("GAMEOVER"));
        Death.gameObject.SetActive(true);
            dead = false;
        
    }

}
