using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
//TDK443
[CreateAssetMenu(fileName = "EnemyDatabase", menuName = "Enemy Data/Enemy Database")]
public class EnemyDatabaseSO : ScriptableObject
{
    [Header("This List Updates Itself Dynamicaly"), SerializeField] 
    EnemyBehaviorSO[] enemyList;

    private void OnEnable() {
    //enemyList = new List<EnemyBehaviorSO>(); //Just in case it needs to be reset on startup
    //Searches for all the EnemyBehaviorSO then checks them one by one
        enemyList = enemyList.Union(Resources.FindObjectsOfTypeAll<EnemyBehaviorSO>()).ToArray();
    }

    public GameObject spawnEnemy(string name, Vector2 location){
        EnemyBehaviorSO enemy = enemyList.First(x => x.enemyName.Contains(name));
        return enemy.spawnEnemy(name, location);;
    }

    public void ClearAllEnemies(){ Array.ForEach(GameObject.FindGameObjectsWithTag("Enemy"), b => Destroy(b));}
}
