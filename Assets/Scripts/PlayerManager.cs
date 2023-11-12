using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Player Reference
    public Player newPlayer;

    void Start()
    {
        // Find the Player script
        newPlayer = GameObject.Find("Player").GetComponent<Player>();
    }

    public void Action(ActionSO action)
    {
        if (action.actionWeight > 0) // Check to make sure the action is not empty
        {
            // Determine if the action is a normal or key action
            if(action.keyAction) // Key action
            {
                newPlayer.MoralityMeter(action.moralityValue, action.actionWeight);
                newPlayer.Reputation(action.keyEvent);
            }
            else // Normal action
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
