using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlieController : EnemyController
{
     // animator varilibles
    public Animator myAnim;
    public override void onAttack(){
   
    // these are inherting base behavaiour from the enemy controller. 
    
        base.onAttack();
        gameObject.GetComponentInChildren<Animator>().SetTrigger("Attack");
    }
    public override void onDeath() {
        base.onDeath();
    }
    public override void onHit(float damage, GameObject source){
        base.onHit(damage, source);
    }
}
