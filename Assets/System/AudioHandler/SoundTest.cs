using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{   

    [SerializeField] AudioControllerSO myController;
    float timer = 0;
    bool startSound = true;
    void FixedUpdate()
    {
         if(timer < 1)

         timer += Time.deltaTime; 

         else if (startSound) {
            startSound = false;
            //This is in place of the game trigger
            gameObject.GetComponent<AudioHandler>().SetPlayList("DayCycle");
            gameObject.GetComponent<AudioHandler>().PlayNext();

            //Play Audio File From Selected Controller
            //myController.Play("Menu Select");
         }

    }



}
