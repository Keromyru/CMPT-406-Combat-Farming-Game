using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyOnDeathNormal", menuName = "Enemy Data/Enemy Actions/OnDeath Normal")]
public class EnemyOnDeath_NormalSO : EnemyOnDeathSO
{   

    [SerializeField] GameObject deathEffect;
    public override void onDeath(GameObject thisObject)
    {
        int dropNum;
        int cashDrop = thisObject.GetComponent<EnemyController>().myEnemyData.cashPerDrop;
        if (thisObject.GetComponent<EnemyController>().myEnemyData.numberOfDropsOnDeath != 0){
            dropNum = thisObject.GetComponent<EnemyController>().myEnemyData.numberOfDropsOnDeath;
        }
        else { dropNum = 1;}
        
        // //TODO ADD TO CURRENCY
        // EIther spawn a bunch of pickup goodies, or just give CASH
        if (deathEffect != null) {Instantiate(deathEffect,thisObject.transform.position,thisObject.transform.rotation);}
        Destroy(thisObject);

    } 
}
