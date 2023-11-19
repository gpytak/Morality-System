using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    // The NPC Dialogue we are currently stepping through
    public DialogueSO currentConversation; // The current conversation found in the DialogueHandler in NPC
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
    private Transform playerObject;
    
    // PlayerManager Reference
    public PlayerManager playerManager;

    // VillagerManager Reference
    public VillagerManager villagerManager;


    // Start is called before the first frame update
    void Start()
    {
        // Find the Player Transform
        playerObject = GameObject.Find("Player").GetComponent<Transform>();
        playerObject.position = new Vector3(-3, -1, 0);

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
                TurnOffDialogue();
            else if(currentConversation.actors[stepNum] == DialogueActors.Continue) // If there is a Continue
            {
                // Find the actor's behavior
                for (int i = 0; i < villagerManager.newVillager.Length; i++)
                {
                    if (villagerManager.newVillager[i].actorSO.actorName == currentConversation.option0.actors[0].ToString())
                    {
                        Continue(playerManager.newPlayer.playerMorality, villagerManager.newVillager[i]); // Call Continue with player morality and the villager
                        break;
                    }
                }
            }
            else if(currentConversation.actors[stepNum] == DialogueActors.TaskStart) // If there is a TaskStart
            {
                // Find the actor
                for (int i = 0; i < villagerManager.newVillager.Length; i++)
                {
                    if (villagerManager.newVillager[i].actorSO.actorName == currentConversation.actors[stepNum-1].ToString())
                    {
                        // Set villager's task start to true
                        villagerManager.newVillager[i].taskStarted = true;
                        break;
                    }
                }
                
                if((stepNum + 1) <= currentConversation.actors.Length)
                    stepNum += 1;
            }
            else if(currentConversation.actors[stepNum] == DialogueActors.TaskComplete) // If there is a TaskComplete
            {
                // Find the actor
                for (int i = 0; i < villagerManager.newVillager.Length; i++)
                {
                    if (villagerManager.newVillager[i].actorSO.actorName == currentConversation.actors[stepNum-1].ToString())
                    {
                        // Set villager's task complete to true
                        villagerManager.newVillager[i].taskComplete = true;
                        break;
                    }
                }

                if((stepNum + 1) <= currentConversation.actors.Length)
                    stepNum += 1;
            }
            else if(currentConversation.actors[stepNum] == DialogueActors.End) // If there is an End
            {
                TurnOffDialogue();
                
                SceneManager.LoadScene("MainMenu");
            }
            else // Continue dialogue
                PlayDialogue();
        }
    }

    void PlayDialogue()
    {
        SetActorInfo(true);
        
        // Display Dialogue
        actor.text = currentSpeaker;
        portrait.sprite = currentPortrait;

        // If there is a branch
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
            if(currentConversation.action0 != null)
                playerManager.Action(currentConversation.action0); // Passes action chosen to PlayerManager
            currentConversation = currentConversation.option0;
        }
        if(optionNum == 1)
        {
            if(currentConversation.action1 != null)
                playerManager.Action(currentConversation.action1); // Passes action chosen to PlayerManager
            currentConversation = currentConversation.option1;
        }
        if(optionNum == 2)
        {
            if(currentConversation.action2 != null)
                playerManager.Action(currentConversation.action2); // Passes action chosen to PlayerManager
            currentConversation = currentConversation.option2;
        }
        if(optionNum == 3)
        {
            if(currentConversation.action3 != null)
                playerManager.Action(currentConversation.action3); // Passes action chosen to PlayerManager
            currentConversation = currentConversation.option3;
        }
        stepNum = 0; // Reset dialogue step counter for new conversation
    }

    public void Continue(int playerMorality, Villager villager)
    {

        if(!villager.taskStarted)
        {
            switch(playerMorality) // option0 = good/neutral, option1 = bad, option2 = empty
            {
                case 1: // Player is good
                    if(villager.behavior == -1)
                        currentConversation = currentConversation.option1; // Sets bad conversation
                    else if (villager.behavior == 0 || villager.behavior == 1)
                        currentConversation = currentConversation.option0; // Sets good/neutral conversation
                    else
                        currentConversation = currentConversation.option2; // Ends conversation
                    break;

                case 0: // Player is neutral
                        currentConversation = currentConversation.option0; // Sets good/neutral conversation
                    break;

                case -1: // Player is bad
                    if(villager.behavior == 1)
                        currentConversation = currentConversation.option2; // Sets empty conversation
                    else if (villager.behavior == 0 || villager.behavior == -1)
                        currentConversation = currentConversation.option0; // Sets good/neutral conversation
                    else
                        currentConversation = currentConversation.option2; // Ends conversation
                    break;

                default:
                    break;
            }
        }
        
        if(villager.taskStarted)
        {
            // Check if the task if finished
            if(!villager.taskFinished) // If the task is not finished
                currentConversation = currentConversation.option0;
            else // If the task is finished
                currentConversation = currentConversation.option1;
        }

        stepNum = 0; // Reset dialogue step counter for new conversation
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
        for (int i = 0; i < villagerManager.newVillager.Length; i++)
        {
            if (villagerManager.newVillager[i].actorSO.actorName == npcDialogue.conversation[0].actors[0].ToString())
            {
                // The array we are currently stepping through
                if (villagerManager.newVillager[i].taskStarted && !villagerManager.newVillager[i].taskComplete) // Returning with task
                    currentConversation = npcDialogue.conversation[1];
                else if (villagerManager.newVillager[i].taskComplete) // Finished task
                    currentConversation = npcDialogue.conversation[2];
                else // First time meeting
                    currentConversation = npcDialogue.conversation[0];
                break;
            }
        }

        dialogueActivated = true;
    }

    public void TurnOffDialogue() // Triggered when leaving NPC in NPCDialogue.cs
    {
        stepNum = 0;
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
    Henry,
    Alice,
    Joseph,
    Branch,  // Selecting this will tell the script that it needs to show the option buttons.
    Continue, // Selecting this will tell the script that it needs to choose the next conversation.
    TaskStart, // Selecting this will tell the script that the task has been started.
    TaskComplete, // Selecting this will tell the script that the task has been complete.
    Brother,
    End
};
