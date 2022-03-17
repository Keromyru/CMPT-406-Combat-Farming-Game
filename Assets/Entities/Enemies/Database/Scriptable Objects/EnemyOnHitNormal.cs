using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyOnHitBehavior", menuName = "Enemy Data/Enemy Actions/OnHit Normal")]
public class EnemyOnHitNormal : EnemyOnHitSO
{
    public override float onHit(float damage, GameObject source, GameObject thisObject){
        return damage;
    }

}
