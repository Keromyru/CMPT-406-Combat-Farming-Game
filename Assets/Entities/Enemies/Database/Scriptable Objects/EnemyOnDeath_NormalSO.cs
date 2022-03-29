using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyOnDeathNormal", menuName = "Enemy Data/Enemy Actions/OnDeath Normal")]
public class EnemyOnDeath_NormalSO : EnemyOnDeathSO
{   

    [SerializeField] GameObject deathEffect;
    public override void onDeath(GameObject thisObject)
    {
        if (deathEffect != null) {Instantiate(deathEffect,thisObject.transform.position,thisObject.transform.rotation);}
        Destroy(thisObject);

    } 
}
