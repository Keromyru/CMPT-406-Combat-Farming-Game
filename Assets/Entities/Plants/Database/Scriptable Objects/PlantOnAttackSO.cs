using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TDK443
public class PlantOnAttackSO : ScriptableObject, IPlantOnAttack
{
    public float attackRange;
    public float attackRate;
    public float attackDamage;
    public GameObject myObject;
    public virtual void OnAttack(float damage, List<GameObject> target, GameObject thisObject){
        myObject = thisObject;
    }

    
    public float fromMe(GameObject baddy){
        return Vector3.Distance(baddy.transform.position, myObject.transform.position);
    }
}
