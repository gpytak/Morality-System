using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

   // Villager References
    public Villager villager;

    private SpriteRenderer speechBubbleRenderer; // Allows speech bubble to turn on and off

    // Start is called before the first frame update
    void Start()
    {
        speechBubbleRenderer = GetComponent<SpriteRenderer>();
        speechBubbleRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player completes the task
        if(Input.GetButtonDown("Interact") && villager.taskStarted && !villager.taskFinished && !villager.taskComplete && speechBubbleRenderer.enabled)
        {
            villager.taskFinished = true;
            speechBubbleRenderer.enabled = false;
        }
    }

    // Called as long as a Collider2D is detected within its trigger range
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && villager.taskStarted && !villager.taskFinished && !villager.taskComplete)
        {
            // Speech bubble on
            speechBubbleRenderer.enabled = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // Speech bubble off
            speechBubbleRenderer.enabled = false;
        }
    }
}
