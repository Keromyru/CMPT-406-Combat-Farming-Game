using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TDK443
public class PlantOnAttackSO : ScriptableObject, IPlantOnAttack
{
    public virtual void OnAttack(float damage, GameObject target){

    }
}
