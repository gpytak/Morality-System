using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformDrop : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)) // If S key is held down then call the fall timer
        {
            StartCoroutine(FallTimer());
        }
    }

    IEnumerator FallTimer() // Waits for set amount of time before player drops
    { 
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.15f);
        GetComponent<CapsuleCollider2D>().enabled = true;
    } 

}
