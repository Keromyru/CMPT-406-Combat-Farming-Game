using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Behavior", menuName = "Player Data/Player Action/OnDeath Teleport")]
public class PlayerOnDeathTeleport : PlayerOnDeathSO
{
    [SerializeField]float DeathPenalty;
    [SerializeField]GameObject deathEffect;
    [SerializeField]Vector2 spawnPoint;
    public override void onDeath(GameObject thisObject) {
        GameObject.Find("Player").GetComponent<IPlayerControl>().PlayerDeath(DeathPenalty,deathEffect,spawnPoint);
    }
}
