using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyControl 
{
    void setHealth(float health);
    float getHealth();
    void setMyEnemyData(EnemyBehaviorSO newEnemyData);
    void setOnHit(EnemyOnHitSO newOnHit);
    void setOnDeath(EnemyOnDeathSO newOneDeath);
    void setOnAttack(EnemyOnAttackSO newOnAttack);
    void setAudioController( AudioControllerSO newAudioController);
    void ApplySlow(float newSlowMulti, float duration);
    
}
