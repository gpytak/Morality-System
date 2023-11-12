using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerMorality = 0; // The morality of the player; 1 = Good, 0 = Neutral, -1 = Bad

    public int bufferMeter = 0; // The value of the buffer meter
    public int moralityMeter = 0; // The value of the morality meter

    public int bufferGood = 0; // Stores the good values in the buffer meter
    public int bufferNeutral = 0; // Stores the neutral values in the buffer meter
    public int bufferBad = 0; // Stores the bad values in the buffer meter

    public string reputationTitle = "Traveler"; // Portray the player based on the reputation value
    public int reputationValue = 0; // Equal to the state of the morality meter

    public string headline = "Headline";


    public void BufferMeter(int moralityValue, int actionWeight) {
        switch(moralityValue) { // Check the morality value of the action
            case 1: // Good action: add action weight to buffer
                if (moralityMeter >= 30) { // Give a bonus to the actionWeight if it aligns with the moralityMeter
                    bufferGood += actionWeight * (1 + (moralityMeter/100));
                    // Debug.Log("Adding actionWeight to bufferGood with bonus");
                } else {
                    bufferGood += actionWeight;
                    // Debug.Log("Adding actionWeight to bufferGood");
                }
                break;
            case 0: // Neutral action: add action weight to buffer
                if (moralityMeter < 30 && moralityMeter > -30) { // Give a bonus to the actionWeight if it aligns with the moralityMeter
                    bufferNeutral += actionWeight * (1 + (moralityMeter/100));
                    // Debug.Log("Adding actionWeight to bufferNeutral with bonus");
                } else {
                    bufferNeutral += actionWeight;
                    // Debug.Log("Adding actionWeight to bufferNeutral");
                }
                break;
            case -1: // Bad action: add action weight to buffer
                if (moralityMeter <= -30) { // Give a bonus to the actionWeight if it aligns with the moralityMeter
                    bufferBad += actionWeight * (1 + (moralityMeter/100));
                    // Debug.Log("Adding actionWeight to bufferBad with bonus");
                } else {
                    bufferBad += actionWeight;
                    // Debug.Log("Adding actionWeight to bufferBad");
                }
                break;
            default:
                break;
        }

        // Update the buffer meter
        bufferMeter = bufferGood + bufferNeutral + bufferBad;

        // Check if the buffer meter is full
        if (bufferMeter >= 20) {
            // Calculate buffer value to add to morality meter
            if (bufferGood > bufferBad && bufferGood > bufferNeutral) {
                MoralityMeter(1, bufferGood); // Call Morality Meter with bufferGood
            } else if (bufferNeutral > bufferGood && bufferNeutral > bufferBad) {
                MoralityMeter(0, bufferNeutral); // Call Morality Meter with bufferNeutral
            } else if (bufferBad > bufferGood && bufferBad > bufferNeutral) {
                MoralityMeter(-1, bufferBad);// Call Morality Meter with bufferBad
            }

            bufferMeter = 0;
            bufferGood = 0;
            bufferNeutral = 0;
            bufferBad = 0;
        }
    }
    
    public void MoralityMeter(int moralityValue, int actionWeight) {
        switch(moralityValue) { // Check the morality value of the action
            case 1: // Good action: add action weight to morality
                if ((moralityMeter + actionWeight) > 100) { // Check if change would exceed 100
                    moralityMeter = 100;
                    // Debug.Log("moralityMeter at +100");
                } else {
                    moralityMeter += actionWeight;
                    // Debug.Log("Adding actionWeight to moralityMeter towards +100");
                }
                break;

            case 0: // Neutral action: move morality towards 0 by action weight
                if (moralityMeter > 0){ // Check if change would exceed 0
                    if ((moralityMeter - actionWeight) < 0) {
                        moralityMeter = 0;
                        // Debug.Log("moralityMeter at 0");
                    } else {
                        moralityMeter -= actionWeight;
                        // Debug.Log("Subtracting actionWeight to moralityMeter towards 0");
                    }
                }
                if (moralityMeter < 0) { // Check if change would exceed 0
                    if ((moralityMeter + actionWeight) > 0) {
                        moralityMeter = 0;
                        // Debug.Log("moralityMeter at 0");
                    } else {
                        moralityMeter += actionWeight;
                        // Debug.Log("Adding actionWeight to moralityMeter towards 0");
                    }
                }
                break;

            case -1: // Bad action: remove action weight from morality
                if ((moralityMeter - actionWeight) < -100) { // Check if change would exceed -100
                    moralityMeter = -100;
                    // Debug.Log("moralityMeter at -100");
                } else {
                    moralityMeter -= actionWeight;
                    // Debug.Log("Subtracting actionWeight to moralityMeter towards -100");
                }
                break;

            default:
                break;
        }

        // Set the player's morality meter; 1 = Good, 0 = Neutral, -1 = Bad
        if (moralityMeter >= 30) {
            playerMorality = 1;
        }
        else if (moralityMeter < 30 && moralityMeter > -30) {
            playerMorality = 0;
        }
        else if (moralityMeter <= -30) {
            playerMorality = -1;
        }
    }

    public void Reputation(string keyEvent)
    {
        headline = keyEvent;
    }

}
