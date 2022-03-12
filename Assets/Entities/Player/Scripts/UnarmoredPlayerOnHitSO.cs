using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Behavior", menuName = "Player Data/Player Action/OnHit Unarmored ")]
public class UnarmoredPlayerOnHitSO : PlayerOnHitSO
{
     public override float onHit(float damage, GameObject source, GameObject thisObject)
    {
        return damage;
    }
}
