using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;


public class NPC : MonoBehaviour
{
    public TextAsset textFile;
    public string loadLines;
    public List<string> textLines = new List<string>();
    public string[] dialogueText;


    private JsonData stringData;
    private int count = 0;
    private void Start()
    {
        if (textFile != null)
        {
            loadLines = File.ReadAllText(Application.dataPath + "/Resources/Dialogue.json");
            stringData = JsonMapper.ToObject(loadLines);

            count = GetString()["Dialogue"].Count;
            for (int i = 0; i < count; i++)
            {
                textLines.Add(GetString()["Dialogue"][i].ToString());
            }
            dialogueText = textLines.ToArray();
        }
    }


    private void OnTriggerStay(Collider other)
    {
            if (Input.GetKeyDown(JPGameManager.GM.jump))
                {
                DialogueSystem.Instance.AddNewDialogue(dialogueText, this.name);
        }
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
