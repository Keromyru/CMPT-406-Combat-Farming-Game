using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Behavior", menuName = "Player Data/Player Behavior Contoller")]
public class PlayerBevahviorSO : ScriptableObject
{
    public float maxHealth;
    public float moveRate;

    //Behaviors
    public PlayerOnAttackSO onAttackBehavior;
    public PlayerOnHitSO onHitBehavior;


     
}
