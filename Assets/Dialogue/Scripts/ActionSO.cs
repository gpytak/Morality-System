using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ActionSO : ScriptableObject
{
    [Header("Action Values")]
    public string moralityValue; // 1 = Good, 0 = Neutral, -1 = Bad
    public float actionWeight; // Measured based on the degree of the importance and permanence

    [Header("Key Actions")]
    public bool keyAction; // Determines if the action is a key action
    [Tooltip("The action's event description")]
    [TextArea]
    public string keyEvent; // A summary of the key action with event details and are used alongside reputations
}
