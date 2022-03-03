using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Melissa's Janky Player Health script
// Can be taken out / edited if the need arises wrote 
// it mostly to test that i could in fact do damage with my
// weird little alien man. 

public class PlayerHealth : MonoBehaviour
{
    // Player Health
    public int maxHealth = 100;
    private static int currentHealth; 
    public int HP;
    
    void Start()
    {
        currentHealth = maxHealth; // makes an instance of the player health
        Time.timeScale = 1; // welp you died, get gud scrub. 

    }



    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 )
        {
            Time.timeScale = 0;
            ResetPlayer();
        }
    }

    public void SavePlayer(){
        HP = currentHealth;
    }

    public void ResetPlayer()
    {
        // i feel like this is self expanitory, but resets the player bk to 100;
        HP = 100;
        currentHealth = 100;
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            TakeDamage(5);
            SavePlayer();
        }
    }
}
