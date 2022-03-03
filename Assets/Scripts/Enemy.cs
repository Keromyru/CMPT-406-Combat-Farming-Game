using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Melissa's Janky enemy Health script. 
// if they take damage they die :) 
// if they die they blow up. 
// is this correct remains to be seen. 

public class Enemy : MonoBehaviour
{
    public int enemyHealth = 100; 
 
     void Start()
    {
        gameObject.tag = "Enemy";
    }

    
   
    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if(enemyHealth <= 0)
        {
            EnemyDeath();
        }
    }
    
    void EnemyDeath(){
    Destroy(gameObject);
    }
}
