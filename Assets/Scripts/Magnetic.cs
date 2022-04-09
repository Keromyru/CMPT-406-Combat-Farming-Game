using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    [Header("Magnetic Behavior")]
    float pickUpRange = 0.5f;
    float magnetRange = 5f;
    [SerializeField] AudioClipSO SoundPickup;
    private GameObject player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate() {
        float distance = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
        
        if (distance < pickUpRange){
            if (SoundPickup != null){SoundPickup.Play();}
                onPickup();
                Destroy(gameObject);
        }
        else if (distance < magnetRange) {            
        //Float towrds player
            float speed = 5/distance * 0.7f;
            gameObject.transform.position =  Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            this.transform.Rotate (0,0,360*Time.deltaTime); //Spins it around because it's funny
        }
    }

    public virtual void onPickup(){

    }
}
