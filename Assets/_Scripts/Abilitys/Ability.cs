using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Ability : ScriptableObject
{
    public Sprite sprite;

    public bool activateOnStart = false;

    public new string name;

    public float coolDownTime;

    public float DeactiveTime;

  


    public virtual void Activate() { }

    public virtual void CallOnlyWhenAbilityEnds() { }

    public virtual void ResetAbilityValuesAnyTime() { }
    
}
