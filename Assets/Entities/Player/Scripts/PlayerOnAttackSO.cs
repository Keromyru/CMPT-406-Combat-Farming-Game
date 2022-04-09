using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This SO differs slightly than the other versions as it takes a Vector 2 location instead of a gameObject
public class PlayerOnAttackSO : ScriptableObject
{
    public float damage;
    public float projectileLife;
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public float firePointLength;
    public float fireRate;
    public Sprite GunSprite;
    private Rigidbody2D rb;



    public virtual void OnAttack(float damage, Vector2 targetLocation, GameObject thisObject){

    }
}
