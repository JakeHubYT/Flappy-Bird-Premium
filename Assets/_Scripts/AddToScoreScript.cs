using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToScoreScript : MonoBehaviour
{
    public AudioClip scoreSound;
    bool canAddPoint = true;



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && canAddPoint)
        {
            //static and is on every pipe so if i colide and 
            Actions.OnCollectPoint();

            AudioManager.Instance.PlaySound(scoreSound);

            
            canAddPoint = false;

            //in anything is subscribed to the event, invoke it
          

        }

       

    }

  


}
