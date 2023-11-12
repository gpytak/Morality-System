using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public DialogueSO[] conversation;

    private Transform player; // Allows the NPC to follow the player's position, so he can turn toward the player
    private SpriteRenderer speechBubbleRenderer; // Allows speech to turn on and off

    private DialogueManager dialogueManager;
    private bool dialogueInitiated;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        speechBubbleRenderer = GetComponent<SpriteRenderer>();
        speechBubbleRenderer.enabled = false;
    }

    // Called as long as a Collider2D is detected within its trigger range
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !dialogueInitiated)
        {
            // Speech bubble on
            speechBubbleRenderer.enabled = true;

            // Find the player's Transform
            player = collision.gameObject.GetComponent<Transform>();

            // Check to see where the player is, and then turn to the player
            if(player.position.x > transform.position.x && transform.parent.localScale.x < 0)
            {
                Flip();
            } 
            else if(player.position.x < transform.position.x && transform.parent.localScale.x > 0)
            {
                Flip();
            }

            dialogueManager.InitiateDialogue(this);
            dialogueInitiated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // Speech bubble off
            speechBubbleRenderer.enabled = false;
            dialogueManager.TurnOffDialogue();
            dialogueInitiated = false;
        }
    }

    private void Flip() 
    {
        Vector3 currentScale = transform.parent.localScale;
        currentScale.x *= -1;
        transform.parent.localScale = currentScale;
    }
}
