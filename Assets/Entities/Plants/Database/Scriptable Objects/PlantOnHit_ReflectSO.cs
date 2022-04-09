using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "OnHit Reflect", menuName = "Plant Data/Plant Action/OnHit Reflect")]
public class PlantOnHit_ReflectSO : PlantOnHitSO
{
    [Range(0,100), SerializeField] float reflectPercentage; 
    public override float onHit(float damage, GameObject source, GameObject thisObject)
    {
        ITakeDamage sITD = source.GetComponent<ITakeDamage>();
        if (sITD != null) {
            sITD.onHit(damage*(reflectPercentage/100f), thisObject);
        }
        return damage;
    }
}
