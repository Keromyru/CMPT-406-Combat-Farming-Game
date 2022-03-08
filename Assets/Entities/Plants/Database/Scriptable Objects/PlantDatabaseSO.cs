using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlantDatabase", menuName = "Plant Data/Plant Database")]

public class PlantDatabaseSO : ScriptableObject
{
    public PlantBehaviorSO test;

    public GameObject spawnPlant(string name, Vector2 location){
        return test.spawnPlant(name, location);
    }

    public GameObject spawnPlant(string name, Vector2 location, float health, float energy){
        GameObject plant = spawnPlant(name,location);

        return plant;
    }

}

