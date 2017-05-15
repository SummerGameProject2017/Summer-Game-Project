using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public Image heart;     //reference for hearts
    public Image heart2;    //heart2
    public Image heart3;    //heart3    

    public int energy = 3; //int for energy hearts
    public Text gameOverText;   //reference for game over text
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

        if (energy == 0)
        {
            heart.enabled = false;
            heart2.enabled = false;
            heart3.enabled = false;
            isGameOver = true;    //set game over to true
            gameOverText.enabled = true; //enable GameOver text
        }

        if (energy == 1)
        {
            heart.enabled = true;
            heart2.enabled = false;
            heart3.enabled = false;
        }

        if (energy == 2)
        {
            heart.enabled = true;
            heart2.enabled = true;
            heart3.enabled = false;
        }

        if (energy == 3)
        {
            heart.enabled = true;
            heart2.enabled = true;
            heart3.enabled = true;
        }
    }
    //Check if player enters/stays on the fire
    void OnTriggerEnter(Collider other)
    {
        //if player triggers fire object and health is greater than 0
        if (other.gameObject.name == "ennemy test" && energy > 0)
        {
            energy -= 1;  //reduce health
            transform.position += new Vector3 (2.0f, 0.0f, 0.0f);
        }

        //for recovering the hearts when picking up cube, oil etc...
        if (other.gameObject.name == "health test" && energy > 0 && energy < 3)
        {
            energy += 1;  //reduce health
            Debug.Log("works");
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