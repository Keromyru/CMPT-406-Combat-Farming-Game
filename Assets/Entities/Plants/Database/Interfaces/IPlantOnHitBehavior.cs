using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TDK443
//This is the primary interface in which all "On hit" SO's should be interacted with
public interface IPlantOnHitBehavior
{
    float onHit(float damage, GameObject source, GameObject thisObject);
}
