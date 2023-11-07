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
        Debug.Log("moralityValue: " + action.moralityValue);
        Debug.Log("actionWeight: " + action.actionWeight);
        if (action.actionWeight > 0) // Check to make sure the action is not empty
        {
            if(action.keyAction) // Determine if the action is normal or key
            { // Key action
                newPlayer.MoralityMeter(action.moralityValue, action.actionWeight);
                newPlayer.Reputation(action.keyEvent);
            }
            else
            { // Normal action
                newPlayer.BufferMeter(action.moralityValue, action.actionWeight);
            }
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
