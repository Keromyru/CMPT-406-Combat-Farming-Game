using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCollection : MonoBehaviour{

    public List<GameObject> plantedPlants;

    void Awake(){
        plantedPlants = new List<GameObject>();
    }

    public void addPlant(GameObject plant){
        plantedPlants.Add(plant);
    }

    public void removePlant(GameObject plant){

    }

    public int getSize(){
        return plantedPlants.Count;
    }
    
    public void findPlant(GameObject plant){

    }
}
