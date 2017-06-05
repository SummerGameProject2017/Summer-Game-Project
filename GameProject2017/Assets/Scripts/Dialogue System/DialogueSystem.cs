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
    public short stringLength = 0;
    Button yesButton, noButton;
    Text dialogueText, nameText;
    public short dialogueIndex;
    public bool startTalking = true;
    short currentCharacterIndex;
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
        dialogueText = dialoguePanel.transform.FindChild("Text").GetComponent<Text>();
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
        if (InputManager.GetButtonDown("Jump") && startTalking == false)//if the dialogue has already started and the jump button is pushed go to the continue dialogue function.
        {
            ContinueDialogue();
        }
        if (currentCharacterIndex >= 2)     
        {
            startTalking = false;
        }

        if (savePanel.activeSelf)   //if the save panel is active start moving controller or keys to change which menu is selected
        {
            playerScript.enabled = false;
            npcScript.enabled = false;
            if (InputManager.GetKeyDown(KeyCode.A) || InputManager.GetKeyDown(KeyCode.LeftArrow) || InputManager.GetAxis("Horizontal") < 0)
            {
                yesButton.image.color = new Color(255, 0, 0);
                noButton.image.color = new Color(255, 255, 255);
                yes = true;
                no = false;
            }
            if (InputManager.GetKeyDown(KeyCode.D) || InputManager.GetKeyDown(KeyCode.RightArrow) || InputManager.GetAxis("Horizontal") > 0)
            {
                noButton.image.color = new Color(255, 0, 0);
                yesButton.image.color = new Color(255, 255, 255);
                no = true;
                yes = false;
            }
            if (yes == true && InputManager.GetButtonDown("Jump"))      //if the jump button is pushed on yes or no save game or not
            {
                YesButtonPushed();
            }
            if (no == true && InputManager.GetButtonDown("Jump"))
            {
                NoButtonPushed();
            }

        }
       if (InputManager.GetKeyDown(KeyCode.G))
        {
            Debug.Log("load");
            SaveLoad.Load();
        }


    }
    public void AddNewDialogue(List<string> lines, string npcName)      //add dialogue from the NPC script into dialogue
    {
        dialogueIndex = 0;
        dialogueText.text = "";
        dialogueLines = new List<string>(lines.Count);
        dialogueLines.AddRange(lines);
        this.npcName = npcName;


        CreateDialogue();
    }

    public void CreateDialogue()        //activate the dialogue panel and show the first line of dialogue
    {

        StartCoroutine(DisplayString(dialogueLines[dialogueIndex]));
        isStringBeingShown = true;
        
        nameText.text = npcName;
        dialoguePanel.SetActive(true);
    }

    public void ContinueDialogue()      
    {
        

            if (isStringBeingShown == false && dialogueIndex < dialogueLines.Count - 1)     //start showing the next lines of dialogue one leter at a time
            {
                isStringBeingShown = true;
                stringLength = 0;
                dialogueText.text = "";

                dialogueIndex++;
                StartCoroutine(DisplayString(dialogueLines[dialogueIndex]));
            }
           else if (isStringBeingShown == true)
            {
            //if the jump key is pushed before the dialogue is sone showing increase speed of text writing to screen
                secondsBetweenDialogue = 0;
        }
        else 
        {   //if the NPC is a savebot activate the save panel
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

    

    //display the string one character at a time
    IEnumerator DisplayString(string stringToDisplay)
    {
        stringLength = (short)stringToDisplay.Length;
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
    //close the save panel and reset the color of the buttons
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
