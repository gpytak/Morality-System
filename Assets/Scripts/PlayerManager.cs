using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Player Reference
    public Player newPlayer;

    // DialogueManager Reference
    public DialogueManager dialogueManager;


    public void Action(ActionSO action)
    {
        // Debug.Log("moralityValue: " + action.moralityValue);
        // Debug.Log("actionWeight: " + action.actionWeight);
        if (action.actionWeight > 0) // Check to make sure the action is not empty
        {
            // Debug.Log("(Before) bufferMeter: " + newPlayer.bufferMeter);
            // Debug.Log("(Before) moralityMeter: " + newPlayer.moralityMeter);
            // Determine if the action is normal or key
            if(action.keyAction) // Key action
            {
                newPlayer.MoralityMeter(action.moralityValue, action.actionWeight);
                // newPlayer.Reputation(action.keyEvent);
            }
            else // Normal action
            {
                newPlayer.BufferMeter(action.moralityValue, action.actionWeight);
            }
            // Debug.Log("(After) bufferMeter: " + newPlayer.bufferMeter);
            // Debug.Log("(After) moralityMeter: " + newPlayer.moralityMeter);
        }
    }


    // public void Game_Menu()
    // {
        
    // }

    // public void Quests()
    // {
        // public void Accept_Quest()
        // {

        // }
    // }

    // public void Inventory()
    // {
        
    // }

    // public void Discovery_Log()
    // {
        
    // }
}
