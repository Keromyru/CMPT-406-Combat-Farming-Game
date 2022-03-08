using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Class Inharits from PlantOnAttackSO, that is not accessible in the editor, and only has virtual classes that should be overwrittem
// This Object also inharits the interface "IPlantOnAttack" so that a generic plant controller can access it if it gets complicated
[CreateAssetMenu(fileName = "OnAttack Ranged", menuName = "Plant Data/Plant Action/OnAttack Ranged Attack")]
public class RangedPlantOnAttackSO : PlantOnAttackSO
{   
   [SerializeField] float range;
   [SerializeField] float damage;
   
   public override void OnAttack(float damage, GameObject target, GameObject thisObject){
   //Enter All Attack Stuff Here    
   }
}
