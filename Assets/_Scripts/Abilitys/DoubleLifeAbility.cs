using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class DoubleLifeAbility : Ability
{
    
    public override void Activate()
    {
        Actions.OnDoubleLife();
    }

    public override void CallOnlyWhenAbilityEnds()
    {
        
    }

    public override void ResetAbilityValuesAnyTime()
    {

        

    }

}
