using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTest : MonoBehaviour
{
    public EnemyDatabaseSO baddies;
    [SerializeField] int numOfBaddies;

    private void Awake() {

        // StartCoroutine(startEnemySpawnage());

        // baddies.spawnEnemy("Crawlie", new Vector2(-12.18753f,1.358912f));
        // // baddies.spawnEnemy("Basic_enemy", new Vector2(-14.37506f,1.701823f));

        // //baddies.spawnEnemy("Basic_enemy", new Vector2(5.18753f,1.358912f));
        // // baddies.spawnEnemy("Basic_enemy", new Vector2(4.37506f,1.701823f));
        // // baddies.spawnEnemy("Basic_enemy", new Vector2(-13.18753f,1.358912f));
        // //baddies.spawnEnemy("Basic_enemy", new Vector2(-11.37506f,1.701823f));
        // // baddies.spawnEnemy("Basic_enemy", new Vector2(-5.18753f,1.358912f));
        // // baddies.spawnEnemy("Basic_enemy", new Vector2(-4.37506f,1.701823f));
    }

    // IEnumerator startEnemySpawnage(){
    //     yield return new WaitForSeconds(20f);
    //     StartCoroutine(spawnEnemiesContinously());
    // }

    // IEnumerator spawnEnemiesContinously(){
    //     // while(true){
            
    //     //     baddies.spawnEnemy("Basic_enemy", new Vector2(-17,3));
    //     //     baddies.spawnEnemy("Basic_enemy", new Vector2(-15,-9));
    //     //     baddies.spawnEnemy("Basic_enemy", new Vector2(-12,8));
    //     //     yield return new WaitForSeconds(3f);
    //     // }

    //     for (int i = 0; i < numOfBaddies; i++)
    //         {
    //             Vector2 offset =  Random.insideUnitCircle * 6;
    //             baddies.spawnEnemy("Crawlie", new Vector2(-10+ offset.x ,1f + offset.y));
    //             yield return new WaitForSeconds(1f);
    //         }
    // }
}
