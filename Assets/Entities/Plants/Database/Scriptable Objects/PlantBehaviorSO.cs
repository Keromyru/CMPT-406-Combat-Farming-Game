using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Plant Behavior", menuName = "Plant Data/Plant Behavior Contoller")]
public class PlantBehaviorSO : ScriptableObject
{
    [Header("Plant Stats")]
    public string plantName;
    public string toolTip;
    public float plantMaxHealth;
    public float plantMaxEnergy;
    public type plantType;
    
    
    [Header("Plant Attack")]
    public bool canAttack;
    public float attackRate;
    public float attackRange;
    public float attackDamage;
   
    
    
    
    [Header("Plant Growth")]
    public bool harvestable;
    public bool canDieOfOldAge;
    public int matureAge;
    public int harvestCycle;
    public int deathAge;
    public int seedDropMin;
    public int seedDropMax;
    public int harvestValue;

    [Header("Audio Files")]
    public string soundDeath;
    public string soundAttack;
    public string soundHarvested;
    public string soundHurt;
    


    
    public GameObject harvestPrefab;
    public GameObject plantPrefab;

    public AudioControllerSO audioController;
    
   

    
    [Header("Plant Behaviors")]
    [SerializeField] PlantOnHitSO onHit;
    [SerializeField] PlantOnAttackSO onAttack;
    [SerializeField] PlantOnDeathSO onDeath;
    [SerializeField] PlantOnHarvestSO onHarvest;




    //A Dropdown list to set the type
    public enum type
    {
        defense,
        produce,
        utility
    }

    public GameObject spawnPlant(string name, Vector2 location){
        //Create Plant from the prefab selected at Designated Coordinate 
        GameObject plant  =  Instantiate(this.plantPrefab, location, Quaternion.identity);
        plant.transform.SetParent(GameObject.Find("ActiveInWorld").transform);
        //Grab the interface of the newly created plant
        IPlantControl plantControl = plant.GetComponent<IPlantControl>();
        //Set Data Sets
        plantControl.setMyPlantData(this);
        plantControl.setAudioController(audioController);
        //Set Behaviors
        plantControl.setOnAttack(onAttack);
        plantControl.setOnHit(onHit);
        plantControl.setOnDeath(onDeath);
        plantControl.setOnHarvest(onHarvest);
        //Set Stats According To Database
        plantControl.setEnergy(plantMaxEnergy);
        plantControl.setHealth(plantMaxHealth);
        plantControl.setGrowAge(0); //It's a baby!
        plantControl.setLocation(location);

        return plant;
    }

    public GameObject spawnPlant(string name, Vector2 location, float health, float energy, int age){
        GameObject plant = spawnPlant(name,location);
        //Grab the interface of the newly created plant
        IPlantControl plantControl = plant.GetComponent<IPlantControl>();
        
        plantControl.setGrowAge(age);
        plantControl.setHealth(health);
        plantControl.setEnergy(energy);


        return plant;
    }

    //GETS 'n SETS
    public string getName(){return plantName;}
    public string getTooltip(){return toolTip;}
}
