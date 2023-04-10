using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class FlipGravityAbility : Ability
{
    
    public override void Activate()
    {
        Actions.OnFlipGravity();
        
    }

    public override void CallOnlyWhenAbilityEnds()
    {

    }

    public override void ResetAbilityValuesAnyTime()
    {
     
        

    }

}
