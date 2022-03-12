using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "PlantDatabase", menuName = "Plant Data/Plant Database")]

public class PlantDatabaseSO : ScriptableObject
{
    [Header("This List Updates Itself Dynamicaly"), SerializeField] List<PlantBehaviorSO> plantList;
    private void OnEnable() {
        //plantList = new List<PlantBehaviorSO>(); //Just in case it needs to be reset on startup
        //Searches for all the PlantBehaviorSO then checks them one by one
        foreach (PlantBehaviorSO item in Resources.FindObjectsOfTypeAll<PlantBehaviorSO>())
        {   //If the PlantBehaviorSO isn't already on the list it'll add it 
            if(!plantList.Contains(item)) {plantList.Add(item);}
        }
    }

    public GameObject spawnPlant(string name, Vector2 location){
        PlantBehaviorSO plant = plantList.Find(x => x.plantName.Contains(name));
        
        return plant.spawnPlant(name, location);
    }
    //This one is for the load system to put things back as is.
    public GameObject spawnPlant(string name, Vector2 location, float health, float energy, int age){
        GameObject plant = spawnPlant(name,location,health, energy, age);

        return plant;
    }


    public string getTooltip(string name){ return plantList.Find(x => x.getName().Contains(name)).getTooltip();}

}

