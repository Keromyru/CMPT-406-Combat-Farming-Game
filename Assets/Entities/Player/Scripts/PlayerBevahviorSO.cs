using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Behavior", menuName = "Player Data/Player Behavior Contoller")]
public class PlayerBevahviorSO : ScriptableObject
{
     [Header("Player Stats")]
    public float maxHealth;
    public float moveRate;
    public float WaterQuantity;
    public float WaterRate;
    public float interactionRange;

    [Header("Audio Files")]
    public string soundDeath;
    public string soundHurt;
    public string soundHeal;
    public string soundWater;
    
    public AudioControllerSO audioController;

    [Header("Behaviors")]
    public PlayerOnAttackSO onAttackBehavior;
    public PlayerOnHitSO onHitBehavior;
    public PlayerOnDeathSO onDeathBehavior;

    [Header("Effects")]
    public GameObject WaterEffect;

    public GameObject EnemyArrow;
    [Tooltip("This should be only visable on the minimap")]
    public GameObject HubArrow;
}
