using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerMorality { get; set; }
    public string playerReputation { get; set; }

    [SerializeField]
    

    void Start()
    {
        pool = GetComponent<UnitPool>();
        player = FindObjectOfType<Player>().transform;
    }
    
    // public void Move()
    // {

    // }

    // public void Talk()
    // {
        
    // }

    // public void Interact()
    // {
        
    // }

    // public void Open_Game_Menu()
    // {
        
    // }

    // public void Accept_Quest()
    // {
        
    // }

    // public void Open_Quests()
    // {
        
    // }

    // public void Open_Inventory()
    // {
        
    // }

    // public void Open_Discovery_Log()
    // {
        
    // }
}
