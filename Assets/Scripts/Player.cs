using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Range(0,10)]
    float moveSpeed = 1f;

    // void Start()
    // {
    //     pool = GetComponent<UnitPool>();
    //     player = FindObjectOfType<Player>().transform;
    // }
    
}
