using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ability : ScriptableObject
{
    public new string name;

    public float coolDownTime;


    public virtual void Activate() { }

    public virtual void Deactivate() { }

    
}
