using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TDK443
/*
This is a public interface that the plant controllers interacts with to configure any of the data of the plant it's trying to spawn;
This was it doesn't matter what the scripts are on the plants so long as they contain this interface
*/
public interface IPlantControl 
{
    //Sets and Gets
    void setHealth(float health);
    float getHealth();
    void setEnergy(float energy);
    float getEnergy();
    void setLocation(Vector3 location);
    Vector3 getLocation();
    void setGrowAge(int age);
    int getGrowAge();
    float getRemaining(); //Get Remaining Water Level

    bool waterPlant(float quantity); //Water Plant Action

    float getMaxHealth();
    float getMaxEnergy();

    bool onHarvest();

    void setOnHit(PlantOnHitSO newOnHit);
    void setOnDeath(PlantOnDeathSO newOneDeath);
    void setOnAttack(PlantOnAttackSO newOnAttack);
    void setOnHarvest(PlantOnHarvestSO newOnHarvest);
    void setAudioController( AudioControllerSO newAudioController);
    void setMyPlantData(PlantBehaviorSO newPlantData);

    

    //Triggers
    void newDay();
    void newNight();



}