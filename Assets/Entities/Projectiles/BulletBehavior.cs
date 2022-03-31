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
    [Header("Projectile Configerations")]
    [Range(0,10)] [SerializeField] float lifeTime = 2.0f; //How long it lasts before it expires
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
    private AudioSource myAudioSource;


    private void Start() 
    {
        if (shotSound != null)  {shotSound.Play(myAudioSource);} // Audio Initiate
    }

    private void OnTriggerEnter2D(Collider2D entity){
        if (entity.tag.Contains("Structure") ||
            entity.tag.Contains("Obstacle")){
            //Debug.Log(entity.name);
            Impacted(entity.gameObject);
        }

    }

    private void OnTriggerStay2D(Collider2D entity) {
        if (ableToImpact.Contains(entity.tag) &&
            entity.gameObject.GetComponent<BoxCollider2D>()){
            if(entity.gameObject.GetComponent<BoxCollider2D>().bounds.Contains(this.transform.position)){
               // Debug.Log(entity.name);
                Impacted(entity.gameObject);
            }
        }
    }

    private float myDistance(Collider2D entity){
        return Vector2.Distance(this.gameObject.transform.position, entity.transform.position);
    }


    private void Impacted(GameObject entity){
        if (impactEffect != null) { //Places an Impact Effect
                    GameObject effect = Instantiate(impactEffect, transform.position, transform.rotation); // For When the bullet impacts
                    Destroy(effect, .25f); //To remove effect after a certain time
                } 
        if (impactSound != null) { impactSound.Play(myAudioSource);}

        if (ableToDamage.Contains(entity.tag)) {Damaged(entity);} //If it Can be damaged

        Destroy(gameObject);
    }

    private void Damaged(GameObject entity){
        ITakeDamage targetDamage = entity.GetComponent<ITakeDamage>(); //Accessing The Interface
        if (targetDamage != null){  targetDamage.onHit(bulletDamage, source);} //If the target has the interface and therefore is damagable
    }
    
    //Deletes bullet if it doens't impact something
    private void FixedUpdate() {
        if(lifeTime > 0) {lifeTime -= Time.deltaTime;}
        else { Destroy(gameObject);}
    }
}
