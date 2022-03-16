using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnHitSO : ScriptableObject
{
    public virtual float onHit(float damage, GameObject source, GameObject thisObject){
        return damage;
    }
}
