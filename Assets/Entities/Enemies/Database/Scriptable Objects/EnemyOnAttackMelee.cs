using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyOnAttackBehavior", menuName = "Enemy Data/Enemy Actions/OnAttack Melee")]
public class EnemyOnAttackMelee : EnemyOnAttackSO
{

    private Vector3 trackedLocation;
    private Vector3 trackedRotation;
    float  firePointLength;
    [SerializeField] GameObject attackEffect;
    public override void OnAttack(float damage, GameObject target, GameObject thisObject){
              //Gets Normalized direction of the target
        Vector3 sourceLocation = thisObject.transform.position;
        Vector3 targetLocation = (target.transform.position);
        Vector3 targetDirection =  (targetLocation - sourceLocation).normalized;
      
        // a Normalized Vector * the distance from the focal point desired + from the source of the 
        trackedLocation = (targetDirection * firePointLength)  + sourceLocation;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x)* Mathf.Rad2Deg - 90f; //Converts the vecter into a RAD angle, then into degrees. Adds 90deg as an offset
        trackedRotation = new Vector3(0,0,angle);

        if (attackEffect != null) {GameObject Effect = Instantiate(attackEffect, sourceLocation, Quaternion.Euler(trackedRotation.x,trackedRotation.y,trackedRotation.z));}

        //Get interface to make sure it has one
        ITakeDamage damagable = target.GetComponent<ITakeDamage>(); 
      
        //if it's not a baddy and can take damage, mess it up
        if( damagable != null && target.tag != "Enemy"){ damagable.onHit(attackDamage,thisObject);}
    }
}
