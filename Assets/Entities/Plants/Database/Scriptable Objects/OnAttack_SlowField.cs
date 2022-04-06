using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "OnAttack SlowField", menuName = "Plant Data/Plant Action/OnAttack SlowField")]
public class OnAttack_SlowField : PlantOnAttackSO

{   
    public float slowDuration = 2;
    public float slowRatio = 0.7f;
    public GameObject zapeffect;
    public override void OnAttack(float damage, List<GameObject> targetList, GameObject thisObject){
    myObject = thisObject;
    Array.ForEach(targetList.ToArray(), b => {
        Debug.Log("SlowField Engaged");
        if (fromMe(b) < attackRange) {
            if(zapeffect != null){Instantiate(zapeffect,b.transform.position,Quaternion.identity);}
            IEnemyControl mybaddy = b.GetComponent<IEnemyControl>();
            if(mybaddy != null){
                mybaddy.ApplySlow(slowRatio, 2);
            }
            b.GetComponent<ITakeDamage>().onHit(damage,thisObject);
        }
    });

   }
}
