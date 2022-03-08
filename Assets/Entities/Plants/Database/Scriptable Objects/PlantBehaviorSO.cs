using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Plant Behavior", menuName = "Plant Data/Plant Behavior Contoller")]
public class PlantBehaviorSO : ScriptableObject
{
    [Header("Plant Stats")]
    [SerializeField] string plantName;
    [SerializeField] string toolTip;
    [SerializeField] float plantHealth;
    [SerializeField] float plantEnergy;
    [SerializeField] bool harvestable;


    [Header("Plant Attack")]
    [SerializeField] bool canAttack;
    [SerializeField] float attackRate;
    [SerializeField] float attackRange;
    [SerializeField] float attackDamage;
    [SerializeField] type plantType;
    [Header("Plant Growth")]

    [Space]
    [SerializeField] GameObject plantPrefab;
    
   

    
    [Header("Plant Behaviors")]
    [SerializeField] PlantOnHitSO onHit;
    [SerializeField] PlantOnAttackSO onAttack;
    [SerializeField] PlantOnDeathSO onDeath;




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
        //Grab the interface of the newly created plant
        IPlantControl plantControl = plant.GetComponent<IPlantControl>();


        //Set Stats According To Database
        plantControl.setMaxEnergy(plantEnergy);
        plantControl.setMaxHealth(plantHealth);
        plantControl.setGrowAge(0); //It's a baby!
        plantControl.setLocation(location);
        plantControl.setType((int)plantType);
        plantControl.setAttackDamage(attackDamage);
        plantControl.setAttackRange(attackRange);
        plantControl.setAttackRate(attackRate);

        plantControl.setOnHit(onHit);
        if (canAttack) { plantControl.setOnAttack(onAttack);}


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
