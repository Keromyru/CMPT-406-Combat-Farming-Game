using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantOnHarvestSO : ScriptableObject, IPlantOnHarvest
{
    [SerializeField] GameObject[] dropPool;
    [SerializeField] int minYield, maxYield;
    [SerializeField] float dropRadius;
    [SerializeField] int credValue;
    [SerializeField] float spewSpeed;
    public virtual void OnHarvest(GameObject thisObject)
    {
        int numOfDrops = Random.Range(minYield,maxYield);
        for (int i = 0; i < numOfDrops; i++) {  //For the number of elements that were chosen to be spawned
            GameObject newDrop = Instantiate(dropPool[Random.Range(0,dropPool.Length-1)], thisObject.transform.position, Quaternion.identity);
            Rigidbody2D rb  = newDrop.GetComponent<Rigidbody2D>();
            Vector3 launch = newDrop.transform.localPosition + Random.insideUnitSphere * spewSpeed;
            rb.AddForce(launch, ForceMode2D.Impulse);
        }  
    }
}
