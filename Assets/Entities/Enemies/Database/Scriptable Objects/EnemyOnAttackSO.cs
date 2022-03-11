using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnAttackSO : ScriptableObject
{
    public float attackRate;
    public float attackDamage;

    public virtual void OnAttack(float damage, GameObject target, GameObject thisObject){

    }
}
