using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Plant Behavior", menuName = "Plant Data/Plant Behavior Contoller")]
public class PlantBehaviorSO : ScriptableObject
{
    [Header("Plant Stats")]
    [SerializeField] string plantName;
    [SerializeField] float plantHealth;
    [SerializeField] float plantEnergy;
    [SerializeField] float plantDamageReduction;
    [SerializeField] bool harvestable;
    [Header("Plant Attack")]
    [SerializeField] bool canAttack;
    [SerializeField] float attackRate;
    [SerializeField] float attackRange;
    [SerializeField] type plantType;
    [Header("Plant Growth")]

    [Space]
    [SerializeField] GameObject plantPrefab;
    
   

    
    [Header("Plant Behaviors")]
    [SerializeField] PlantOnHitSO onHit;
    [SerializeField] PlantOnAttackSO onAttack;




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

        plantControl.setOnHit(onHit);
        if (canAttack) { plantControl.setOnAttack(onAttack);}


        return plant;
    }

    public GameObject spawnPlant(string name, Vector2 location, float health, float energy){
        GameObject plant = spawnPlant(name,location);

        return plant;
    }
}
