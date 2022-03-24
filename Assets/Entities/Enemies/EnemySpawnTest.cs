using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTest : MonoBehaviour
{
    public EnemyDatabaseSO baddies;
    [SerializeField] int numOfBaddies;

    private void Awake() {
        // // baddies.spawnEnemy("Basic_enemy", new Vector2(5.18753f,1.358912f));
        // // baddies.spawnEnemy("Basic_enemy", new Vector2(4.37506f,1.701823f));
        // baddies.spawnEnemy("Basic_enemy", new Vector2(-13.18753f,1.358912f));
        //baddies.spawnEnemy("Basic_enemy", new Vector2(-11.37506f,1.701823f));
        // // baddies.spawnEnemy("Basic_enemy", new Vector2(-5.18753f,1.358912f));
        // // baddies.spawnEnemy("Basic_enemy", new Vector2(-4.37506f,1.701823f));
    
        for (int i = 0; i < numOfBaddies; i++)
                {
                    Vector2 offset =  Random.insideUnitCircle * 6;
                    baddies.spawnEnemy("Basic_enemy", new Vector2(-10+ offset.x ,1f + offset.y));
                }
    }

}
