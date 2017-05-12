using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public Image heart;     //reference for hearts
    public Image heart2;    //heart2
    public Image heart3;    //heart3    
    public Object player;   //not currenltly  used//only for testing//can delete

    private int energy = 3; //int for energy
    public Text gameOverText;   //reference for text
    private bool isGameOver = false; //a bool flag to see if game is over

    void Start()
    {
        gameOverText.enabled = false; //disables the GameOver text on start
    }

    // Update is called once per frame
    void Update()
    {
        //check if game is over i.e., health is greater than 0
        if (!isGameOver)
            transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * 10f, 0, 0); //get input
        Debug.Log(energy);
    }

    //Check if player enters/stays on the fire
    void OnTriggerEnter(Collider other)
    {
        //if player triggers fire object and health is greater than 0
        if (other.gameObject.name == "ennemy test" && energy > 0)
        {
            energy -= 1;  //reduce health
            transform.position += new Vector3 (2.0f, 0.0f, 0.0f);

            //reverse forward vector
            //hides the right heart on screen when player get hits once
            heart3.enabled = false;

            //loses second heart when hit a second time
            if (energy == 1)
            {
                heart2.enabled = false;
            }

            //loses third heart when hit third time
            if (energy == 0)
            {
                heart.enabled = false;
                isGameOver = true;    //set game over to true
                gameOverText.enabled = true; //enable GameOver text
            }

        }

        //for recovering the hearts when picking up cube, oil etc...
        if (other.gameObject.name == "health test" && energy > 0)
        {
            energy += 1;  //reduce health

           // transform.position += new Vector3(2.0f, 0.0f, 0.0f);

            //hides the right heart on screen when player get hits once
            heart2.enabled = true;
            Debug.Log("heart2");

            //loses second heart when hit a second time
            if (energy == 2)
            {
                heart3.enabled = true;
                Debug.Log("heart3");
            }
        }
    }

        //this temporarily stops the player after colliding with ennemy(cube)
        void OnTriggerExit()
        {
      /*  
        for (int i = 0; i < 1000; i++)
        {
            Debug.Log("For LOOP");
        }
   */
    }
   
}