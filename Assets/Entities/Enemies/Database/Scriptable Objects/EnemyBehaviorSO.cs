using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyBehavior", menuName = "Enemy Data/Enemy Behavior Contoller")]
public class EnemyBehaviorSO : ScriptableObject
{
    [Header("Enemy Stats")]
    public string enemyName;
    public float enemyMaxHealth;

    public int numberOfDropsOnDeath;
    public int cashPerDrop;
    
    
    [Header("Enemy Attack")]
    public float attackRate;
    public float attackRange;
    public float attackDamage;

    [Header("Target Preferences")]
    public List<TargetSO> priorityList;

    [Header("Audio Files")]
    public string SoundOnHit;
    public string SoundOnAttack;
    public string SoundOnDeath;
    public string SoundAggression;
    


    public GameObject enemyPrefab;
    public AudioControllerSO audioController;

    
      public GameObject spawnEnemy(string name, Vector2 location){
        //Create Plant from the prefab selected at Designated Coordinate 
        GameObject plant  =  Instantiate(this.enemyPrefab, location, Quaternion.identity);
        plant.transform.SetParent(GameObject.Find("ActiveInWorld").transform);
        //Grab the interface of the newly created plant
        IPlantControl plantControl = plant.GetComponent<IPlantControl>();
        //Set Data Sets
        // plantControl.setMyEnemyData(this);
        plantControl.setAudioController(audioController);

        return plant;
    }

}
