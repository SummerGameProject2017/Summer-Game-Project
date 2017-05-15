using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public TextAsset textFile;
    public string[] textLines;

    public string[] dialogue;
    public string name;

    private void Start()
    {
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(JPGameManager.GM.jump))
            {
                DialogueSystem.Instance.AddNewDialogue(textLines, name);
        }
    }

   


}
