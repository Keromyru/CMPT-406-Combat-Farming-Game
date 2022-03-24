using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StriderController : EnemyController
{
    private void Start() {
        Debug.Log(myEnemyData.name);
    }
    public override void onAttack(){
        base.onAttack();
    }
    public override void onDeath() {
        base.onDeath();
    }

    public override void onHit(float damage, GameObject source){
        base.onHit(damage, source);
    }
}
