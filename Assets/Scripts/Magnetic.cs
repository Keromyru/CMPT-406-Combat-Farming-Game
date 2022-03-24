using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    [SerializeField] float pickUpRange = 1.0f;
    [SerializeField] AudioClipSO SoundPickup;
    private GameObject player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate() {
        this.transform.Rotate (0,0,360*Time.deltaTime); //Spins it around because it's funny
        float distance = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
        if (distance < pickUpRange){
            if (SoundPickup != null){SoundPickup.Play();}
                onPickup();
                Destroy(gameObject);
        }
                       
        //Float towrds player
            float speed = 5/distance * 2;
            gameObject.transform.position =  Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            //Scale Pickup As it approaches
            if (distance < 1){
                transform.localScale = new Vector3(distance/2,distance/2, 0);
            } 
    }

    public virtual void onPickup(){

    }
}
