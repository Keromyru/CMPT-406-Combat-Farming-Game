using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "PlantDatabase", menuName = "Plant Data/Plant Database")]

public class PlantDatabaseSO : ScriptableObject
{
    [SerializeField] GameObject SpawnInEffect; 
    [Header("This List Updates Itself Dynamicaly"), SerializeField] 
    PlantBehaviorSO[] plantList;
    private void OnEnable() {
        //Searches for all the PlantBehaviorSO then checks them one by one
        plantList = plantList.Union(Resources.FindObjectsOfTypeAll<PlantBehaviorSO>()).ToArray();
    }

    public GameObject spawnPlant(string name, Vector2 location){
        if (SpawnInEffect != null) {Instantiate(SpawnInEffect, location, Quaternion.identity);}
        PlantBehaviorSO plant = plantList.First(x => x.plantName.Contains(name));
        return plant.spawnPlant(name, location);
    }
    //This one is for the load system to put things back as is.
    public GameObject spawnPlant(string name, Vector2 location, float health, int age){
        GameObject plant = spawnPlant(name,location,health, age);

        return plant;
    }

    public string getTooltip(string name){ return plantList.First(x => x.getName().Contains(name)).getTooltip();}
}


