using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static GameObject[] persistentObjects = new GameObject[2]; // Array which holds each persistent object
    public int objectIndex; // Index for each persistent object : Player=0, PauseMenu=1

    // Called immediately before the Start() method
    void Awake()
    {
        if(persistentObjects[objectIndex] == null)
        {
            persistentObjects[objectIndex] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if(persistentObjects[objectIndex] != gameObject)
            Destroy(gameObject);
    }
}
