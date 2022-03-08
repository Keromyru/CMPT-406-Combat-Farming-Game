using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantOnDeathSO : ScriptableObject, IPlantOnDeath
{
    public virtual void onDeath(GameObject thisObject)
    {
        
    }
}
