using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ActionSO : ScriptableObject
{
    public int moralityValue; // 1 = Good, 0 = Neutral, -1 = Bad
    public float actionWeight; // Measured based on the degree of the importance and permanence

    public bool keyAction; // Determines if the action is a key action
    [Tooltip("Key action's event description")]
    [TextArea]
    public string keyEvent; // A summary of the key action with event details and are used alongside reputations
}
