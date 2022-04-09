using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyBehavior", menuName = "Enemy Data/Enemy Behavior Contoller")]
public class EnemyBehaviorSO : ScriptableObject
{
  [Header("Enemy Stats")]
  public string enemyName;
  public float enemyMaxHealth;
  public float enemyMoveSpeed;
  [Range(1,100)]
  public float AdditiveLerpRange = 10;
  [Header("Enemy Attack")]
  [Tooltip("This is time between attacks, lower is faster")]
  public float attackRate;
  public float attackRange;
  public float attackDamage;

  [Header("Target Preferences")]
  public TargetPriority[] priorityList;

  [Header("Audio Files")]
  public string SoundOnHit;
  public string SoundOnAttack;
  public string SoundOnDeath;
  public string SoundAggression;

  [Header("Bevahiors")]
  public EnemyOnAttackSO onAttackBehavior;
  public EnemyOnHitSO onHitBehavior;
  public EnemyOnDeathSO onDeathBehavior;


  public GameObject enemyPrefab;
  public AudioControllerSO audioController;


  public GameObject spawnEnemy(string name, Vector2 location){
    //Create Enemy from the prefab selected at Designated Coordinate 
    GameObject enemy  =  Instantiate(this.enemyPrefab, location, Quaternion.identity);
    enemy.transform.SetParent(GameObject.Find("ActiveInWorld").transform);
    //Grab the interface of the newly created Enemy
    IEnemyControl enemyControl = enemy.GetComponent<IEnemyControl>();
    //Set Data Sets
    enemyControl.setMyEnemyData(this);
    enemyControl.setOnHit(onHitBehavior);
    enemyControl.setOnDeath(onDeathBehavior);
    enemyControl.setOnAttack(onAttackBehavior);
    enemyControl.setAudioController(audioController);

    enemyControl.setHealth(enemyMaxHealth);

    return enemy;
  }

}
