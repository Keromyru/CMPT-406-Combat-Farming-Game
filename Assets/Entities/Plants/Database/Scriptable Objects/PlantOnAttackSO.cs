using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TDK443
public class PlantOnAttackSO : ScriptableObject, IPlantOnAttack
{
    public float attackRange;
    public float attackRate;
    public float attackDamage;
    public virtual void OnAttack(float damage, GameObject target, GameObject thisObject){

    }
}
