using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public Image heart;  //reference for hearts
    public Image heart2;
    public Image heart3;
    public Object player;




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


    IEnumerator Delay()
    {
        print(Time.time);
        yield return new WaitForSecondsRealtime(5);
        print(Time.time);
    }

    //Check if player enters/stays on the fire
    void OnTriggerEnter(Collider other)
    {
        //if player triggers fire object and health is greater than 0
        if (other.gameObject.name == "ennemy test" && energy > 0)
        {
            energy -= 1;  //reduce health
            transform.position += new Vector3 (2.0f, 0.0f, 0.0f);

            StartCoroutine(Delay());
            
            //reverse forward vector
            heart3.enabled = false;

            if (energy == 1)
            {
                heart2.enabled = false;
            }

            if (energy == 0)
            {
                heart.enabled = false;
                isGameOver = true;    //set game over to true
                gameOverText.enabled = true; //enable GameOver text
            }

        }
    }
}