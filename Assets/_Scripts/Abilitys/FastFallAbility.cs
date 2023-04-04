using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class FastFallAbility : Ability
{
    public float fallForce = 5;

    public override void Activate()
    {
       var playerMov =  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        //change fall speed
        //if (playerMov != null)
        playerMov.rb.AddForce(Vector3.down * fallForce, ForceMode.Force);
    }

    public override void Deactivate()
    {
        
    }


}
