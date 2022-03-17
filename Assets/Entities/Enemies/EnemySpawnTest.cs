using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTest : MonoBehaviour
{
    public EnemyDatabaseSO baddies;

    private void Awake() {

        StartCoroutine(startEnemySpawnage());

        // baddies.spawnEnemy("Basic_enemy", new Vector2(-12.18753f,1.358912f));
        // baddies.spawnEnemy("Basic_enemy", new Vector2(-14.37506f,1.701823f));
    }

    IEnumerator startEnemySpawnage(){
        yield return new WaitForSeconds(20f);
        StartCoroutine(spawnEnemiesContinously());
    }

    IEnumerator spawnEnemiesContinously(){
        while(true){
            baddies.spawnEnemy("Basic_enemy", new Vector2(-12.18753f,1.358912f));
            baddies.spawnEnemy("Basic_enemy", new Vector2(-9,-8));
            baddies.spawnEnemy("Basic_enemy", new Vector2(-9,8));
            yield return new WaitForSeconds(2f);
        }
       
    }
}
