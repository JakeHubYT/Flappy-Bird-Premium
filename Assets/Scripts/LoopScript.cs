using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopScript : MonoBehaviour
{
     float objectWidth;
    Vector3 startPos;


    void Start()
    {
        objectWidth = gameObject.GetComponent<MeshRenderer>().bounds.size.x;
        startPos = transform.position;
    }

   
    void Update()
    {
        if(transform.position.x > objectWidth)
        {

            transform.position = startPos;
        }
    }
}
