using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class FastFallAbility : Ability
{
    public float fallForce = 5;

    public override void Activate()
    {
        Actions.OnFastFall();
    }

    public override void CallOnlyWhenAbilityEnds()
    {
        
    }


}
