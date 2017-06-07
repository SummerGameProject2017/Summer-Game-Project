using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;  //needed for JSon data loading


public class NPC : MonoBehaviour
{
    public string loadLines;
    public List<string> textLines = new List<string>();


    private JsonData stringData;
    private short count = 0;
    Transform player;
    DialogueSystem dialogueScript;
    PlayerController playerScript;

    private void Start()
    {
        
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        dialogueScript = GameObject.Find("DialogueSystem").GetComponent<DialogueSystem>(); 
            loadLines = File.ReadAllText(Application.dataPath + "/Resources/Dialogue.json");    //load the dialogue file from the resource folder
            stringData = JsonMapper.ToObject(loadLines);

            count = (short)GetString()["Dialogue"].Count;
            for (int i = 0; i < count; i++)
            {
                textLines.Add(GetString()["Dialogue"][i].ToString());
            }
            //put the dialogue lines into an array that can be sent to the dialogue system
    }


    void Update()
    {       //find the player. If the player is close enough to the NPC start the dialogue
        player = GameObject.FindWithTag("Player").transform;
        float offset = Vector3.Distance(transform.position, player.position);
        if (playerScript.isTalking == true)
        {
            if (InputManager.GetButtonDown("Jump") && offset < 3 && dialogueScript.isTalking == false)  //if the character isnt already talking send the dialogue aray to the Dialogue system to display text
            {
                playerScript.enabled = false;
                dialogueScript.isTalking = true;
                DialogueSystem.Instance.AddNewDialogue(textLines, this.name);
                playerScript.isTalking = false;
            }
        }

          
       
    }
    private void OnTriggerEnter(Collider other)
    {
        playerScript.isTalking = true;
    }
    private void OnTriggerExit(Collider other)
    {
        playerScript.isTalking = false;
    }
    //load the data from the Json file and convert to string getting the variables we need for dialogue
    JsonData GetString()            
    {
        for (int i = 0; i < stringData["Character"].Count; i++)
        {
            if (stringData["Character"][i]["name"].ToString() == this.name)
            {
                return stringData["Character"][i];
            }
        }

        return null;
    }
   


}
