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

    public override void Deactivate()
    {
        Actions.OnVulnerable();
    }

}
