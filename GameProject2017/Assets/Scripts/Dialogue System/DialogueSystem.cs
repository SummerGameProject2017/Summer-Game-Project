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
    public GameObject savePanel;
    public float secondsBetweenDialogue = 0.15f;
    public int stringLength = 0;
    Button yesButton, noButton;
    Text dialogueText, nameText;
    public int dialogueIndex;
    public bool startTalking = true;
    int currentCharacterIndex;
    public bool isStringBeingShown = false;
    public bool isTalking = false;
    PlayerController playerScript;
    bool yes = false;
    bool no = false;
    NPC npcScript;

    void Awake()
    {
        npcScript = GameObject.Find("SaveBot").GetComponent<NPC>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        dialogueText = dialoguePanel.transform.FindChild("Text").GetComponent<Text>(); ;
        nameText = dialoguePanel.transform.FindChild("Name").GetChild(0).GetComponent<Text>();
        dialoguePanel.SetActive(false);

        yesButton = savePanel.transform.FindChild("Yes").GetComponent<Button>();
        noButton = savePanel.transform.FindChild("No").GetComponent<Button>();
        savePanel.SetActive(false);
        yesButton.onClick.AddListener(delegate { YesButtonPushed(); });
        noButton.onClick.AddListener(delegate { NoButtonPushed(); });



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

        if (savePanel.activeSelf)
        {
            playerScript.enabled = false;
            npcScript.enabled = false;
            if (InputManager.GetKeyDown(KeyCode.A) || InputManager.GetKeyDown(KeyCode.LeftArrow))
            {
                yesButton.image.color = new Color(255, 0, 0);
                noButton.image.color = new Color(255, 255, 255);
                yes = true;
                no = false;
            }
            if (InputManager.GetKeyDown(KeyCode.D) || InputManager.GetKeyDown(KeyCode.RightArrow))
            {
                noButton.image.color = new Color(255, 0, 0);
                yesButton.image.color = new Color(255, 255, 255);
                no = true;
                yes = false;
            }
            if (yes == true && InputManager.GetButtonDown("Jump"))
            {
                YesButtonPushed();
            }
            if (no == true && InputManager.GetButtonDown("Jump"))
            {
                NoButtonPushed();
            }

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
            if (npcName == "SaveBot")
            {
                savePanel.SetActive(true);
            }
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

    public void NoButtonPushed()
    {
        npcScript.enabled = true;
        savePanel.SetActive(false);
        playerScript.enabled = true;
        noButton.image.color = new Color(255, 255, 255);
        yesButton.image.color = new Color(255, 255, 255);
        no = false;
    }
    public void YesButtonPushed()
    {
        npcScript.enabled = true;
        SaveLoad.Save();
        savePanel.SetActive(false);
        playerScript.enabled = true;
        noButton.image.color = new Color(255, 255, 255);
        yesButton.image.color = new Color(255, 255, 255);
        yes = false;
    }
}
