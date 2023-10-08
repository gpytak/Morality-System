using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MoralityMeter : MonoBehaviour
{
    void ChangeMorality(int moralityValue, float actionWeight, float playerMorality)
    {
        switch(moralityValue) // Check the morality value of the action
        {
            case 1: // Good action: add action weight to morality
                if ((playerMorality + actionWeight) > 100) { // Check if change would exceed 100
                    playerMorality = 100;
                } else {
                    playerMorality += actionWeight;
                }
                break;

            case 0: // Neutral action: move morality towards 0 by action weight
                if (playerMorality > 0){ // Check if change would exceed 0
                    if ((playerMorality - actionWeight) < 0) {
                        playerMorality = 0;
                    } else {
                        playerMorality -= actionWeight;
                    }
                }
                if (playerMorality < 0) { // Check if change would exceed 0
                    if ((playerMorality + actionWeight) > 0) {
                        playerMorality = 0;
                    } else {
                        playerMorality += actionWeight;
                    }
                }
                break;

            case -1: // Bad action: remove action weight from morality
                if ((playerMorality - actionWeight) < -100) { // Check if change would exceed -100
                    playerMorality = -100;
                } else {
                    playerMorality -= actionWeight;
                }
                break;

            default:
                break;
        }
    }
}
