using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class InvincibleAbility : Ability
{
    
    public override void Activate()
    {
        Actions.OnInvulnerable();
    }

    //for reseting specific to when ability ends
    public override void CallOnlyWhenAbilityEnds()
    {
      
    }

    //for reseting basic values 
    public override void ResetAbilityValuesAnyTime()
    {
        Actions.OnVulnerable();

    }


}
