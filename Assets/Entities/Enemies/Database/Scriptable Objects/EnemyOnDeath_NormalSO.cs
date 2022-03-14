using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnDeath_NormalSO : EnemyOnDeathSO
{
    public override void onDeath(GameObject thisObject)
    {
        int dropNum;
        int cashDrop = thisObject.myEnemyData.cashPerDrop();
        if (thisObject.myEnemyData.numberOfDropsOnDeath() != null){
            dropNum = thisObject.myEnemyData.numberOfDropsOnDeath();
        }
        else {
            dropNum = 1;
        }
        
        


    } 
}
