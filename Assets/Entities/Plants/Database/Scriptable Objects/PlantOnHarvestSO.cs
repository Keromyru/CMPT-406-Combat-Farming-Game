using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantOnHarvestSO : ScriptableObject, IPlantOnHarvest
{
    [SerializeField] GameObject fruit;
    [SerializeField] int minYield, maxYieldl;
    [SerializeField] int credValue;
    public virtual void OnHarvest(GameObject thisObject)
    {
        
    }
}
