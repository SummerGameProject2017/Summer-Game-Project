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
    public int stringLength = 0;
    Button continueButton;
    Text dialogueText, nameText;
    public int dialogueIndex;
    public bool startTalking = true;
    int currentCharacterIndex;
    public bool isStringBeingShown = false;
    public bool isTalking = false;
    PlayerController playerScript;

    void Awake()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        dialogueText = dialoguePanel.transform.FindChild("Text").GetComponent<Text>(); ;
        nameText = dialoguePanel.transform.FindChild("Name").GetChild(0).GetComponent<Text>();
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

    private void Update()
    {
        if (InputManager.GetButtonDown("Jump") && startTalking == false)
        {
            ContinueDialogue();
        }
        if (currentCharacterIndex >= 2)
        {
            startTalking = false;
        }



    }
    public void AddNewDialogue(List<string> lines, string npcName)
    {
        dialogueIndex = 0;
        dialogueText.text = "";
        dialogueLines = new List<string>(lines.Count);
        dialogueLines.AddRange(lines);
        this.npcName = npcName;


        CreateDialogue();
    }

    public void CreateDialogue()
    {

        StartCoroutine(DisplayString(dialogueLines[dialogueIndex]));
        isStringBeingShown = true;
        
        nameText.text = npcName;
        dialoguePanel.SetActive(true);
    }

    public void ContinueDialogue()
    {
        

            if (isStringBeingShown == false && dialogueIndex < dialogueLines.Count - 1)
            {
                isStringBeingShown = true;
                stringLength = 0;
                dialogueText.text = "";

                dialogueIndex++;
                StartCoroutine(DisplayString(dialogueLines[dialogueIndex]));
            }
           else if (isStringBeingShown == true)
            {
                secondsBetweenDialogue = 0;
        }
        else 
        {
            currentCharacterIndex = 0;
            startTalking = true;
            dialoguePanel.SetActive(false);
            isTalking = false;
            playerScript.enabled = true;

            
        }
    }

    


    IEnumerator DisplayString(string stringToDisplay)
    {
        stringLength = stringToDisplay.Length;
        currentCharacterIndex = 0;

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

        
        isStringBeingShown = false;
        secondsBetweenDialogue = 0.15f;

    }
}
