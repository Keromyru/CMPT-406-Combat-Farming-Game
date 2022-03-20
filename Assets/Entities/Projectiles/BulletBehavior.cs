using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//TDK443
//This Script Can Probably be either used, or inharited by other scripts.
//It applies damage via the ITakeDamage Interface, and passes on the origin as a value
// You can check the Assets/Entities/Plants/Database/Scriptable Objects/RangedPlantOnAttackSO.cs for a reference on how to access it.
public class BulletBehavior : MonoBehaviour
{
    [Range(0,10)]
    private float lifeTime = 2.0f; //How long it lasts before it expires
    [Header("Add Tags To These Arrays To Set")]
    [SerializeField] string[] ableToImpact;
    [SerializeField] string[] ableToDamage;

    [Header("Audio - Optional")]
    public GameObject impactEffect; //An Effect that triggers when the projectile hits
    public AudioClipSO impactSound; //Audio Played on impact
    public AudioClipSO shotSound; //Audio Played on shot
    [Header("Do not set these")]
    public float bulletDamage;
    public GameObject source; 


    private void Start() 
    {
        if (shotSound != null)  {shotSound.Play();} // Audio Initiate
    }

    private void OnTriggerEnter2D(Collider2D entity) {
        if (ableToImpact.Contains(entity.tag)){
            if (impactEffect != null) {
                GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation); // For When the bullet impacts
                Destroy(effect, .25f); //To remove effect after a certain time
            } 
            //Play Impact Sound
            if (impactSound != null) { impactSound.Play();}
            
            
            if (ableToDamage.Contains(entity.tag)){
                ITakeDamage targetDamage = entity.GetComponent<ITakeDamage>(); //Accessing The Interface
                if (targetDamage != null){ //If the target has the interface and therefore is damagable
                    targetDamage.onHit(bulletDamage, source);
                }
            }
            Destroy(gameObject);   //then remove this object
        }
    }

    
    //Deletes bullet if it doens't impact something
    private void Update() {
        if(lifeTime > 0) {lifeTime -= Time.deltaTime;}
        else { Destroy(gameObject);}
    }
}
