using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDatabase", menuName = "Enemy Data/Enemy Database")]
public class EnemyDatabaseSO : ScriptableObject
{
    [Header("This List Updates Itself Dynamicaly"), SerializeField] List<EnemyBehaviorSO> enemyList;

    private void OnEnable() {
    //enemyList = new List<EnemyBehaviorSO>(); //Just in case it needs to be reset on startup
    //Searches for all the EnemyBehaviorSO then checks them one by one
        foreach (EnemyBehaviorSO item in Resources.FindObjectsOfTypeAll<EnemyBehaviorSO>())
        {   //If the PlantBehaviorSO isn't already on the list it'll add it 
            if(!enemyList.Contains(item)) {enemyList.Add(item);}
        }
    }

    public GameObject spawnPlant(string name, Vector2 location){
        EnemyBehaviorSO enemy = enemyList.Find(x => x.enemyName.Contains(name));
        return enemy.spawnEnemy(name, location);;
    }
}
