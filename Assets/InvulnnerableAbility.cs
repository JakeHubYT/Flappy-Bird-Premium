using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InvulnnerableAbility : Ability

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
