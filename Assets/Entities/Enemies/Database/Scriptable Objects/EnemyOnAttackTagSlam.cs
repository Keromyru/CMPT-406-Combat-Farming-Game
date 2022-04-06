using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyOnAttackTagSlam", menuName = "Enemy Data/Enemy Actions/OnAttack Tag Slam")]
public class EnemyOnAttackTagSlam : EnemyOnAttackSO
{
    private Vector3 trackedLocation;
    private Vector3 trackedRotation;
    [Header("Configerations")]
    [SerializeField] string myTag;
    [SerializeField] float damageMultiplier = 1;
    [SerializeField] AudioClipSO soundSlam;
    [SerializeField]  float  firePointLength;
    [SerializeField] GameObject attackEffect;
    [SerializeField] GameObject attackEffectSlam;


     public override void OnAttack(float damage, GameObject target, GameObject thisObject){

        // a Normalized Vector * the distance from the focal point desired + from the source of the 
        Vector3 sLocation = thisObject.transform.position; //Source of effect location
        Vector3 tLocation = target.transform.position;
        Vector3 targetDirection =  (tLocation - sLocation).normalized; //Direction

        Vector3 trackedLocation = (targetDirection * firePointLength)  + sLocation;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x)* Mathf.Rad2Deg - 180; //Converts the vecter into a RAD angle, then into degrees. Adds 90deg as an offset
        Quaternion trackedRotation =   Quaternion.Euler(0,0,angle);

        float myDamage = damage;
        //Get interface to make sure it has one
        ITakeDamage damagable = target.GetComponent<ITakeDamage>(); 
        if (target.tag == myTag){ 
            myDamage = damage * damageMultiplier; //Multiplies damage
            if (attackEffectSlam != null) { GameObject Effect = Instantiate(attackEffectSlam, trackedLocation,  trackedRotation);}
            if(soundSlam != null) { soundSlam.Play();}
        } else {
            if (attackEffect != null) {GameObject Effect = Instantiate(attackEffect, trackedLocation,  trackedRotation);}
        }
        //if it's not a baddy and can take damage, mess it up
        if( damagable != null && target.tag != "Enemy"){ damagable.onHit(damage,thisObject);}
    }
}
