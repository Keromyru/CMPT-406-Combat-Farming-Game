using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnHitSO : ScriptableObject
{
    public virtual float onHit(float damage, GameObject source, GameObject thisObject)
    {
        return damage;
    }
}
