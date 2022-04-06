using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StriderController : EnemyController
{
    // these are inherting base behavaiour from the enemy controller. 
    public override void onAttack(){
        gameObject.GetComponentInChildren<Animator>().SetTrigger("Attack");
        base.onAttack();
    }
    public override void onDeath() {
        base.onDeath();
    }

    public override void onHit(float damage, GameObject source){
        base.onHit(damage, source);
    }
}
