using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "OnHit Armored", menuName = "Plant Data/Plant Action/OnHit Armored")]
public class ArmoredPlantOnhitSO : PlantOnHitSO
{
    [Header("Modifier Stats")]
    [SerializeField, Range(0,100)] float damageReductionPercentage;
        public override float onHit(float damage, GameObject source, GameObject thisObject)
    {
        
        return (damageReductionPercentage/100)*damage;
    }
}
