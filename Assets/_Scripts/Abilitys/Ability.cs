using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ability : ScriptableObject
{
    public bool activateOnStart = false;

    public new string name;

    public float coolDownTime;

    public float DeactiveTime;

  


    public virtual void Activate() { }

    public virtual void CallOnlyWhenAbilityEnds() { }

    public virtual void ResetAbilityValuesAnyTime() { }
    
}
