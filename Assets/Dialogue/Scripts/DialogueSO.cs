using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueSO : ScriptableObject
{
    [Header("Actors")]
    public DialogueActors[] actors;

    [Tooltip("Only needed if Random is selected as the actor name")]
    [Header("Random Actor Info")]
    public string randomActorName;
    public Sprite randomActorPortrait;

    [Header("Dialogue")]
    [TextArea]
    public string[] dialogue;

    [Tooltip("The words that will appear on the option buttons")]
    public string[] optionText;

    [Header("Options")]
    public DialogueSO option0;
    public DialogueSO option1;
    public DialogueSO option2;
    public DialogueSO option3;

    [Header("Actions")]
    public ActionSO action0;
    public ActionSO action1;
    public ActionSO action2;
    public ActionSO action3;
}
