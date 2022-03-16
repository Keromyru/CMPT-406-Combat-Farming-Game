using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HubController : MonoBehaviour, ITakeDamage
{
    
    private float hubHealth = 1;
    [SerializeField] float hubMaxHealth;
    UnityEvent event_HubAttacked = new UnityEvent();

    GameObject onDeathEffect;
    AudioClipSO onHitSound;
    AudioClipSO onDeathSound;
    public void onHit(float damage, GameObject source)
    {
        if(source.tag == "Enemy"){ }
        hubHealth -= damage;
        if (onHitSound != null) { onHitSound.Play();} //Play onDeathSound
        event_HubAttacked.Invoke(); //
        if( hubHealth < 0){ onDeath();} //Check Death State
    }

    private void onDeath(){
        if (onDeathEffect != null){ Instantiate(onDeathEffect, this.transform.position, this.transform.rotation); }
        if (onDeathSound != null) { onDeathSound.Play();} //Play onDeathSound
        Debug.Log("You have lost the game my dude");
    }

}
