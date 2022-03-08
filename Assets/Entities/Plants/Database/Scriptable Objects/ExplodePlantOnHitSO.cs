using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TDK443
//This is a test protocal, don't worry about it too much
[CreateAssetMenu(fileName = "OnHit Explode", menuName = "Plant Data/Plant Action/OnHit Explode")]
public class ExplodePlantOnHitSO : PlantOnHitSO
{
    [SerializeField] float Damage;
    [SerializeField] float Range;

    public override void onHit(float damage, GameObject source)
    {
        //Do stuff here
    }
}
