using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VillagerManager : MonoBehaviour
{
    // Villager References
    public Villager[] newVillager; // Array of villagers in VillagerManager gameobject

    // PlayerManager Reference
    public PlayerManager playerManager;

    // DialogueManager Reference
    public DialogueManager dialogueManager;

    public void Start() // Change this to a function thats called when the scene is created?
    {
        // Randomly generate the behavior of the villager at start
        for (int i = 0; i < newVillager.Length; i++)
        {   
            // Check to make sure there's a variety of behaviors
            if (i != 0 && (newVillager[i-1].behavior == -1 || newVillager[i-1].behavior == 1))
                newVillager[i].behavior = 0;
            else
                newVillager[i].behavior = Random.Range(-1, 2);
            
            Debug.Log(newVillager[i].actorSO.actorName + ": " + newVillager[i].behavior);
        }
    }
}
