using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyOnAttackBehavior", menuName = "Enemy Data/Enemy Actions/OnAttack Melee")]
public class EnemyOnAttackMelee : EnemyOnAttackSO
{

    private Vector3 trackedLocation;
    private Vector3 trackedRotation;
    [SerializeField]  float  firePointLength;
    [SerializeField] GameObject attackEffect;
    public override void OnAttack(float damage, GameObject target, GameObject thisObject){

        // a Normalized Vector * the distance from the focal point desired + from the source of the 
        Vector3 sLocation = thisObject.transform.position; //Source of effect location
        Vector3 tLocation = target.transform.position;
        Vector3 targetDirection =  (tLocation - sLocation).normalized; //Direction

        Vector3 trackedLocation = (targetDirection * firePointLength)  + sLocation;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x)* Mathf.Rad2Deg - 180; //Converts the vecter into a RAD angle, then into degrees. Adds 90deg as an offset
        Quaternion trackedRotation =   Quaternion.Euler(0,0,angle);
        if (attackEffect != null) {
            GameObject Effect = Instantiate(attackEffect, trackedLocation,  trackedRotation);
            }
        //Get interface to make sure it has one
        ITakeDamage damagable = target.GetComponent<ITakeDamage>(); 
      
        //if it's not a baddy and can take damage, mess it up
        if( damagable != null && target.tag != "Enemy"){ damagable.onHit(damage,thisObject);}
    }
}
