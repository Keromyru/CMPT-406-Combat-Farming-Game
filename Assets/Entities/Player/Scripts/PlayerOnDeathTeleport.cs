using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Behavior", menuName = "Player Data/Player Action/OnDeath Teleport")]
public class PlayerOnDeathTeleport : PlayerOnDeathSO
{
    [SerializeField]GameObject deathEffect;
    [SerializeField]Vector2 spawnPoint;
    public override void onDeath(GameObject thisObject) {
        if(deathEffect != null){ Instantiate(deathEffect, thisObject.transform.position, Quaternion.identity);}
        base.onDeath(thisObject);
        thisObject.transform.position = spawnPoint;
        GameObject.Find("Player").GetComponent<IPlayerControl>().resetHealth();
    }
}
