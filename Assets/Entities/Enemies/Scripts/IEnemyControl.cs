using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyControl 
{
    void setHealth(float health);
    float getHealth();




    // void setMyPlantData(PlantBehaviorSO newPlantData);
    // void setOnHit(PlantOnHitSO newOnHit);
    // void setOnDeath(PlantOnDeathSO newOneDeath);
    // void setOnAttack(PlantOnAttackSO newOnAttack);
    void setAudioController( AudioControllerSO newAudioController);
    
}
