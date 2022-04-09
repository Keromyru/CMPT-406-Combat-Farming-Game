using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyOnHitTagArmorBehavior", menuName = "Enemy Data/Enemy Actions/OnHit Tag Armor")]
public class EnemyOnHitTagArmor : EnemyOnHitSO
{ //TDK443
    [SerializeField] string myTag;
    [SerializeField, Range(0,100)] float DamageReductionPercentage;

    public override float onHit(float damage, GameObject source, GameObject thisObject){
        float myDamage = damage;
        if(source.tag == myTag) {
            myDamage -= (damage*(DamageReductionPercentage/100));
        }
        return myDamage;
    }
}
