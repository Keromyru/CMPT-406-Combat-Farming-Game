using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HubController : MonoBehaviour, ITakeDamage
{
    [Header("Hub Configeration")]
    public float hubMaxHealth;
    private Healthbar_HUBController myHealthbar;
    private float hubHealth = 1;
    GameObject onDeathEffect;
    AudioClipSO onHitSound;
    AudioClipSO onDeathSound;

    [SerializeField] GameObject endGameObject;
    
    private void Start() {
        myHealthbar = GetComponentInChildren<Healthbar_HUBController>();
        hubHealth = hubMaxHealth;
    }
    public void onHit(float damage, GameObject source){
        hubHealth -= damage;
        if (onHitSound != null) { onHitSound.Play();} //Play onDeathSound
        myHealthbar.updateHB((int)hubHealth);
        if( hubHealth < 0){ onDeath();} //Check Death State
        
    }

    private void onDeath(){
        if (onDeathEffect != null){ Instantiate(onDeathEffect, this.transform.position, this.transform.rotation); }
        if (onDeathSound != null) { onDeathSound.Play();} //Play onDeathSound
        Debug.Log("You have lost the game my dude");
        endGameObject.GetComponent<script_EndgameController>().setStats();
        endGameObject.SetActive(true);
    }

    public void Restore(){
        hubHealth = hubMaxHealth;
    }

    public int getHealth(){
        return (int)hubHealth;
    }
}

public static class MyHub{
    public static int Health(){ return GameObject.Find("Hub").GetComponent<HubController>().getHealth(); }
    public static void Restore(){GameObject.Find("Hub").GetComponent<HubController>().Restore(); } 

}
