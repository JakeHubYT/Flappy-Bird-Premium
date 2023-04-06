using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class SkipStartAbility : Ability
{
    
    public override void Activate()
    {

        Actions.OnSkipStart();  
    }

    public override void CallOnlyWhenAbilityEnds()
    {
        Actions.OnSkipStartEnd();
    }

    public override void ResetAbilityValuesAnyTime()
    {
     
        Actions.OnSkipStartResetValues();

    }

}
