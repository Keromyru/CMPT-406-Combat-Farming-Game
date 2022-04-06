using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionIndicator : MonoBehaviour
{
   [SerializeField] GameObject WateringCan;
   [SerializeField] GameObject RayGun;
    void OnEnable() {
        RayGun.SetActive(false);
        DayNightCycle.isNowDay += newDay;
        DayNightCycle.isNowNight += newNight;
        } //Subscribe to on Scene Loaded Event

    void OnDisable() {
        DayNightCycle.isNowDay -= newDay;
        DayNightCycle.isNowNight -= newNight;
        } //unsubscribe to on Scene Loaded Event



    private void newDay(){
        RayGun.SetActive(false);
        WateringCan.SetActive(true);
    }
    private void newNight(){
        RayGun.SetActive(true);
        WateringCan.SetActive(false);
    }
}
