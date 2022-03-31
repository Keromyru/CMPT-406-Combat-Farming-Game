using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TDK443
public class WateringCan : MonoBehaviour
{
    private Coroutine waterRoutine; 
    private bool isWaiting; //Is in a state of waiting before it can attack again 
    [SerializeField] float WaterQuantity = 1f;
    [SerializeField] float Interval = 0.5f;
    [SerializeField] AudioClipSO SoundWatering;
    public void Water(GameObject myPlant){
        IPlantControl myPlantControl = myPlant.GetComponent<IPlantControl>();
        if (myPlantControl != null &&  //Controller Exists in this object;
            !this.isWaiting  &&     //Is not waiting
            myPlantControl.getRemaining() > 0f) { //Can be watered
            myPlantControl.waterPlant(WaterQuantity);   //Water plant
            if (SoundWatering != null) {SoundWatering.Play();} //Play sound if there is one
        }
        WaterTimer();//START TIMER 
    }
       ////////////////////////////////////////////////
    // Attack Interval Coroutine

    //Starts attack Timer
     public void WaterTimer(){
        if (waterRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple AttackRoutine at the same time would cause bugs.
            StopCoroutine(waterRoutine);
        }
        // Start the Coroutine, and store the reference for it.
        waterRoutine = StartCoroutine(WaterRoutine());            
     }

    private IEnumerator WaterRoutine(){
        //toggles on the screen shake function
        isWaiting = true;
        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(Interval);
        // After the pause, swap back to the original material.
        isWaiting = false;
        // Set the routine to null, signaling that it's finished.
        waterRoutine = null;
    }


}
