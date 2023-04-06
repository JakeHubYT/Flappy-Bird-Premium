using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class DoubleCoinsAbility : Ability
{
    
    public override void Activate()
    {

        Actions.OnDoubleCoins();
    }

    public override void CallOnlyWhenAbilityEnds()
    {
       

    }

    public override void ResetAbilityValuesAnyTime()
    {
        Actions.OnDoubleCoinsEnd();
    }

}
