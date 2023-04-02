using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{

   
    
    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag == "Player" && PipeSpawner.Instance.canDamage)
        {
          
                Actions.OnPlayerDeath();

        }
    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag == "Player" && PipeSpawner.Instance.canDamage)
        {

            Actions.OnPlayerDeath();

        }
    }



}

