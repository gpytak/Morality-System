using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum DialogueActors // Place in actors
{
    Player,
    NPC0,
    NPC1,
    Random, // You will select this for any non-recurring characters you want to use.
    Branch  // Selecting this will tell your script that it needs to show the option buttons.
};
