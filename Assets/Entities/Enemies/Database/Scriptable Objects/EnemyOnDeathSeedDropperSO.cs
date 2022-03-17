using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyOnDeathSeedDrop", menuName = "Enemy Data/Enemy Actions/OnDeath Seed Dropper")]
public class EnemyOnDeathSeedDropperSO : EnemyOnDeathSO
{
    [SerializeField] GameObject[] seeds;
    public override void onDeath(GameObject thisObject)
    {
        Instantiate(
            seeds[Random.Range(0,seeds.Length -1)],
            thisObject.transform.position,
            Quaternion.identity);
    }

}
