using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; set; }
    public List<string> dialogueLines = new List<string>();
    public string npcName;
    public GameObject dialoguePanel;

    public float secondsBetweenDialogue = 0.15f;

    Button continueButton;
    Text dialogueText, nameText;
    int dialogueIndex;

    void Awake()
    {
        continueButton = dialoguePanel.transform.FindChild("Continue").GetComponent<Button>();
        dialogueText = dialoguePanel.transform.FindChild("Text").GetComponent<Text>(); ;
        nameText = dialoguePanel.transform.FindChild("Name").GetChild(0).GetComponent<Text>();
        continueButton.onClick.AddListener(delegate { ContinueDialogue(); });
        dialoguePanel.SetActive(false);



        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void AddNewDialogue(string[] lines, string npcName)
    {
        dialogueIndex = 0;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);
        this.npcName = npcName;


        CreateDialogue();
    }

    public void CreateDialogue()
    {
        nameText.text = npcName;
        dialoguePanel.SetActive(true);
        StartCoroutine(DisplayString(dialogueLines[dialogueIndex]));
    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Count - 1)
        {
            dialogueIndex++;
            StartCoroutine(DisplayString(dialogueLines[dialogueIndex]));
        }
        else
        {
            dialoguePanel.SetActive(false);
            
        }
    }


    IEnumerator DisplayString(string stringToDisplay)
    {
        int stringLength = stringToDisplay.Length;
        int currentCharacterIndex = 0;

        dialogueText.text = "";

        while (currentCharacterIndex < stringLength)
        {
            dialogueText.text += stringToDisplay[currentCharacterIndex];
            currentCharacterIndex++;
            if (currentCharacterIndex < stringLength)
            {
                yield return new WaitForSeconds(secondsBetweenDialogue);
            }
            else
            {
                break;
            }

        }
    }
}
