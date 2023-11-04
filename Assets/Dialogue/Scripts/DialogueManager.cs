using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // The NPC Dialogue we are currently stepping through
    private DialogueSO currentConversation; // The current conversation found in the DialogueHandler in NPC
    private int stepNum; // Tracks which step in the dialogue we're in
    private bool dialogueActivated; // Lets Update() know when to display text

    // UI References
    private GameObject dialogueCanvas;
    private TMP_Text actor;
    private Image portrait;
    private TMP_Text dialogueText;

    // ActorSO References
    private string currentSpeaker;
    private Sprite currentPortrait;
    public ActorSO[] actorSO; // Array which holds the actors

    // Button References
    [SerializeField]
    private GameObject[] optionButton; // Array which holds the buttons inputed in the DialogueManger game object
    private TMP_Text[] optionButtonText;
    private GameObject optionsPanel;

    // Typewriter effect
    [SerializeField]
    private float typingSpeed = 0.02f;
    private Coroutine typeWriterRoutine;
    private bool canContinueText = true;

    // Player Freeze
    private PlayerMovement playerMovement;



    // Start is called before the first frame update
    void Start()
    {
        // Find PlayerMovement script
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        optionsPanel = GameObject.Find("OptionsPanel");
        optionsPanel.SetActive(false);

        // Find the TMP Text on the Buttons
        optionButtonText = new TMP_Text[optionButton.Length]; // Sets array to the number of buttons it will be working with
        for(int i = 0; i < optionButton.Length; i++)
        {
            optionButtonText[i] = optionButton[i].GetComponentInChildren<TMP_Text>(); // Assigns to option buttons
        }

        // Turn off the buttons to start
        for(int i = 0; i < optionButton.Length; i++)
        {
            optionButton[i].SetActive(false);
        }

        dialogueCanvas = GameObject.Find("DialogueCanvas");
        actor = GameObject.Find("ActorText").GetComponent<TMP_Text>();
        portrait = GameObject.Find("Portrait").GetComponent<Image>();
        dialogueText = GameObject.Find("DialogueText").GetComponent<TMP_Text>();
        
        dialogueCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueActivated && Input.GetButtonDown("Interact") && canContinueText)
        {
            // Freeze the player during conversation
            playerMovement.enabled = false;

            // Cancel dialogue if there are no lines of dialogue lines remaining
            if(stepNum >= currentConversation.actors.Length)
            {
                TurnOffDialogue();
            }
            else // Continue dialogue
            {
                PlayDialogue();
            }
        }
    }

    void PlayDialogue()
    {
        // If it's a random NPC
        if(currentConversation.actors[stepNum] == DialogueActors.Random)
        {
            SetActorInfo(false);
        }
        else // If it's a recurring character
        {
            SetActorInfo(true);
        }
        
        // Display Dialogue
        actor.text = currentSpeaker;
        portrait.sprite = currentPortrait;

        // If there is a branch..
        if(currentConversation.actors[stepNum] == DialogueActors.Branch)
        {
            for(int i = 0; i < currentConversation.optionText.Length; i++){
                if(currentConversation.optionText[i] == null) // No text
                {
                    optionButton[i].SetActive(false);
                }
                else
                {
                    optionButtonText[i].text = currentConversation.optionText[i]; // Set the button's text based on what is typed in the NPC dialogue
                    optionButton[i].SetActive(true);
                }
            }
            // Set the first button to be auto-selected
            optionButton[0].GetComponent<Button>().Select();
        }

        // Keep the typewriter routine from running multiple times at the same time
        if(typeWriterRoutine != null)
        {
            StopCoroutine(typeWriterRoutine);
        }

        // Check the step in conversation for displaying dialogue text or display buttons
        if(stepNum < currentConversation.dialogue.Length)
        {
            typeWriterRoutine = StartCoroutine(TypeWriterEffect(dialogueText.text = currentConversation.dialogue[stepNum]));
        }
        else // For when the conversation reaches the branch
        {
            optionsPanel.SetActive(true);
        }

        dialogueCanvas.SetActive(true);
        stepNum += 1;
    }

    void SetActorInfo(bool recurringCharacter)
    {
        if(recurringCharacter) // If it's a recurring character
        {
            for (int i = 0; i < actorSO.Length; i++)
            {
                if(actorSO[i].name == currentConversation.actors[stepNum].ToString())
                {
                    currentSpeaker = actorSO[i].actorName;
                    currentPortrait = actorSO[i].actorPortrait;
                }
            }
        }
        else // If it's a random NPC
        {
            currentSpeaker = currentConversation.randomActorName;
            currentPortrait = currentConversation.randomActorPortrait;
        }
    }

    public void Option(int optionNum)
    {
        foreach (GameObject button in optionButton)
        {
            button.SetActive(false);
        }
        if(optionNum == 0)
        {
            // Debug.Log("optionNum0, currentConversation: '" + currentConversation.option0 + "'");
            Action(currentConversation.action0);
            currentConversation = currentConversation.option0;
        }
        if(optionNum == 1)
        {
            // Debug.Log("optionNum1, currentConversation: '" + currentConversation.option1 + "'");
            Action(currentConversation.action1);
            currentConversation = currentConversation.option1;
        }
        if(optionNum == 2)
        {
            // Debug.Log("optionNum2, currentConversation: '" + currentConversation.option2 + "'");
            Action(currentConversation.action2);
            currentConversation = currentConversation.option2;
        }
        if(optionNum == 3)
        {
            // Debug.Log("optionNum3, currentConversation: '" + currentConversation.option3 + "'");
            Action(currentConversation.action3);
            currentConversation = currentConversation.option3;
        }

        stepNum = 0; // Reset dialogue step counter for new conversation
    }

    private void Action(ActionSO action)
    {
        Debug.Log("moralityValue: " + action.moralityValue);
        Debug.Log("actionWeighte: " + action.actionWeight);

        if (action.actionWeight > 0) // Check to make sure the action is not empty
        {
            
        }

    }

    private IEnumerator TypeWriterEffect(string line) // Takes in line of dialogue
    {
        dialogueText.text = ""; // Clears out currtent text so our text begins as empty.
        canContinueText = false;
        yield return new WaitForSeconds(.5f);
        foreach (char letter in line.ToCharArray())
        {
            if(Input.GetButtonDown("Interact"))
            {
                dialogueText.text = line;
                break;
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        canContinueText = true;
    }


    public void InitiateDialogue(NPCDialogue npcDialogue) // Used in NPCDialogue.cs
    {
        // The array we are currently stepping through
        currentConversation = npcDialogue.conversation[0];
        // Debug.Log("Started conversation: " + currentConversation);

        dialogueActivated = true;
    }

    public void TurnOffDialogue() // Triggered when leaving NPC in NPCDialogue.cs
    {
        stepNum = 0;
        // Debug.Log("Ended conversation. Reset the step to " + stepNum);

        dialogueActivated = false;
        optionsPanel.SetActive(false);
        dialogueCanvas.SetActive(false);

        // Unfreeze the player after conversation
        playerMovement.enabled = true;
    }

}

public enum DialogueActors // Place in actors
{
    Player,
    NPC0,
    NPC1,
    Random, // You will select this for any non-recurring characters you want to use.
    Branch  // Selecting this will tell your script that it needs to show the option buttons.
};
