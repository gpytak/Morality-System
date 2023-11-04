using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferMeter
{
    void UpdateBuffer(int moralityValue, float actionWeight, float playerMorality, float bufferValue, float bufferGood, float bufferNeutral, float bufferBad)
    {
        switch(moralityValue) // Check the morality value of the action
        {
            case 1: // Good action: add action weight to buffer
                if (playerMorality >= 30) { // Give a bonus to the actionWeight if it aligns with the playerMorality
                    bufferGood += actionWeight * (1 + (playerMorality/100));
                    Console.Write("Adding actionWeight to bufferGood with bonus");
                } else {
                    bufferGood += actionWeight;
                    Console.Write("Adding actionWeight to bufferGood");
                }
                break;

            case 0: // Neutral action: add action weight to buffer
                if (playerMorality < 30 && playerMorality > -30) { // Give a bonus to the actionWeight if it aligns with the playerMorality
                    bufferNeutral += actionWeight * (1 + (playerMorality/100));
                    Console.Write("Adding actionWeight to bufferNeutral with bonus");
                } else {
                    bufferNeutral += actionWeight;
                    Console.Write("Adding actionWeight to bufferNeutral");
                }
                break;

            case -1: // Bad action: add action weight to buffer
                if (playerMorality <= -30) { // Give a bonus to the actionWeight if it aligns with the playerMorality
                    bufferBad += actionWeight * (1 + (playerMorality/100));
                    Console.Write("Adding actionWeight to bufferBad with bonus");
                } else {
                    bufferBad += actionWeight;
                    Console.Write("Adding actionWeight to bufferBad");
                }
                break;

            default:
                break;
        }

        bufferValue = bufferGood + bufferNeutral + bufferBad;

        if (bufferValue >= 20) {
            // Calculate buffer value to add to morality meter
            if (bufferGood > bufferBad && bufferGood > bufferNeutral) {
                // Call Morality Meter with bufferGood

            } else if (bufferNeutral > bufferGood && bufferNeutral > bufferBad) {
                // Call Morality Meter with bufferNeutral

            }  else if (bufferBad > bufferGood && bufferBad > bufferNeutral) {
                // Call Morality Meter with bufferBad
                
            }

            bufferValue = 0;
            bufferGood = 0;
            bufferNeutral = 0;
            bufferBad = 0;
        }
    }
}
