using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;


public class NPC : MonoBehaviour
{
    public string loadLines;
    public List<string> textLines = new List<string>();


    private JsonData stringData;
    private int count = 0;
    Transform player;
    DialogueSystem dialogueScript;
    PlayerController playerScript;

    private void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        dialogueScript = GameObject.Find("DialogueSystem").GetComponent<DialogueSystem>(); 
            loadLines = File.ReadAllText(Application.dataPath + "/Resources/Dialogue.json");
            stringData = JsonMapper.ToObject(loadLines);

            count = GetString()["Dialogue"].Count;
            for (int i = 0; i < count; i++)
            {
                textLines.Add(GetString()["Dialogue"][i].ToString());
            }
    }


    void Update()
    {
        player = GameObject.FindWithTag("Player").transform;
        float offset = Vector3.Distance(transform.position, player.position);
            if (InputManager.GetButtonDown("Jump") && offset < 3 && dialogueScript.isTalking == false)
                {
            playerScript.enabled = false;
            dialogueScript.isTalking = true;
                DialogueSystem.Instance.AddNewDialogue(textLines, this.name);
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
