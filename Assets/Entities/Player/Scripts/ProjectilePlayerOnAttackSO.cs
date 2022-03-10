using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Behavior", menuName = "Player Data/Player Action/OnAttack Ranged ")]
public class ProjectilePlayerOnAttackSO : PlayerOnAttackSO
{
    public override void OnAttack(float damage, Vector2 targetLocation, GameObject thisObject){
      //Gets Normalized direction of the target
      Vector3 sLocation = thisObject.transform.position;
      Vector3 tLocation = new Vector3(targetLocation.x,targetLocation.y,0);
      Vector3 targetDirection =  (sLocation - tLocation).normalized;
      
      // a Normalized Vector * the distance from the focal point desired + from the source of the 
      Vector3 trackedLocation = (targetDirection * firePointLength)  + sLocation;
      float angle = Mathf.Atan2(targetDirection.y, targetDirection.x)* Mathf.Rad2Deg - 90f; //Converts the vecter into a RAD angle, then into degrees. Adds 90deg as an offset
      Vector3 trackedRotation = new Vector3(0,0,angle);

      GameObject projectile = Instantiate(projectilePrefab, sLocation, Quaternion.Euler(trackedRotation.x,trackedRotation.y,trackedRotation.z));
      //Set projectile damage here
      projectile.GetComponent<BulletBehavior>().bulletDamage = damage;
      projectile.GetComponent<BulletBehavior>().source = thisObject;

      Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>(); //gets the force component
      rb.AddForce(projectile.transform.up*projectileSpeed, ForceMode2D.Impulse); //Applies an impulse force in an up direction
    }
}
