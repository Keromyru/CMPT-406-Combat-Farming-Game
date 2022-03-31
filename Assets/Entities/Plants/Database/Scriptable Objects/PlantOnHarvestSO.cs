using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantOnHarvestSO : ScriptableObject, IPlantOnHarvest
{
    [SerializeField] GameObject[] dropPool;
    [SerializeField] int minYield, maxYield;
    [SerializeField] float dropRadius;
    [SerializeField] int credValue;
    [SerializeField] float Spread;
    public virtual void OnHarvest(GameObject thisObject)
    {
        int numOfDrops = Random.Range(minYield,maxYield);
        for (int i = 0; i < numOfDrops; i++) {  //For the number of elements that were chosen to be spawned
            Vector3 SpawnSpread = Random.insideUnitSphere * Spread;
            SpawnSpread.z = 0;
            GameObject newDrop = Instantiate(dropPool[Random.Range(0,dropPool.Length-1)], thisObject.transform.position + SpawnSpread, Quaternion.identity);
        } 
        thisObject.GetComponent<PlantController>().onDeath(); 
    }
}
