using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class DirtSurfurAbility : Ability
{
   

    public override void Activate()
    {
        Actions.OnDirtSurf();
       
    }

    public override void CallOnlyWhenAbilityEnds()
    {
        
    }

    public override void ResetAbilityValuesAnyTime()
    {
     
        

    }

}
