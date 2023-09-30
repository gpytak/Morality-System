using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    float z = -10; //Used to maintain the camera's z level, sometimes important for rendering

    void Start()
    {
        if(!target)
        {
            enabled = false;
            Debug.Log("No target");
        }
    }

    void Update()
    {
        z = transform.position.z;
        Vector3 v = target.position;
        v.z = z;
        transform.position = v;
    }
}