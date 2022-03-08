using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlantOnHitSO : ScriptableObject, IPlantOnHitBehavior
{
    public virtual void onHit(float damage, GameObject source)
    {
        
    }
}
