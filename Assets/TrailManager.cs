using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    public Transform trail;

    public Transform trailStart;
    public Transform trailEnd;


    

    // Update is called once per frame
    void Update()
    {
        trail.position = trailStart.position;
        trail.position = trailEnd.position;
    }
}
