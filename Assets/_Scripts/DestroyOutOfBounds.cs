using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float xBounds = -10;
   

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > xBounds)
        {
            Destroy(gameObject);    
        }
    }
}
