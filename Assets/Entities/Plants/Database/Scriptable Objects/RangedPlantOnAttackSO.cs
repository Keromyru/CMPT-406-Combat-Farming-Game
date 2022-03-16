using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Class Inharits from PlantOnAttackSO, that is not accessible in the editor, and only has virtual classes that should be overwrittem
// This Object also inharits the interface "IPlantOnAttack" so that a generic plant controller can access it if it gets complicated
[CreateAssetMenu(fileName = "OnAttack Ranged", menuName = "Plant Data/Plant Action/OnAttack Ranged Attack")]
public class RangedPlantOnAttackSO : PlantOnAttackSO
{   
   [SerializeField] float projectileSpeed;
   [SerializeField] GameObject projectilePrefab;
   [SerializeField] float firePointLength;
   [SerializeField] Vector2 offset;

   private Vector3 trackedLocation;
   private Vector3 trackedRotation;
   private Rigidbody2D rb;

   public override void OnAttack(float damage, GameObject target, GameObject thisObject){

      
      //Gets Normalized direction of the target
      Vector3 sourceLocation = thisObject.transform.position + new Vector3(offset.x,offset.y,0);
      Vector3 targetLocation = (target.transform.position);
      Vector3 targetDirection =  (targetLocation - sourceLocation).normalized;
      
      // a Normalized Vector * the distance from the focal point desired + from the source of the 
      trackedLocation = (targetDirection * firePointLength)  + sourceLocation;
      float angle = Mathf.Atan2(targetDirection.y, targetDirection.x)* Mathf.Rad2Deg - 90f; //Converts the vecter into a RAD angle, then into degrees. Adds 90deg as an offset
      trackedRotation = new Vector3(0,0,angle);

      GameObject projectile = Instantiate(projectilePrefab, sourceLocation, Quaternion.Euler(trackedRotation.x,trackedRotation.y,trackedRotation.z));
      //Set projectile damage here
      projectile.GetComponent<BulletBehavior>().bulletDamage = damage;
      projectile.GetComponent<BulletBehavior>().source = thisObject;

      Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>(); //gets the force component
      rb.AddForce(projectile.transform.up*projectileSpeed, ForceMode2D.Impulse); //Applies an impulse force in an up direction
   }

}
