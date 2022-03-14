using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlantOnHitSO : ScriptableObject, IPlantOnHitBehavior
{
    public virtual float onHit(float damage, GameObject source, GameObject thisObject)
    {
        return damage;
    }
}
