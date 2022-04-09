using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TDK443
public class SessionController : MonoBehaviour
{
    public CursorsSO myCursor;
    void Start() {
        if(GameObject.Find("LightingHandler").GetComponentInChildren<DayNightCycle>() != null){ 
            if(GameObject.Find("LightingHandler").GetComponentInChildren<DayNightCycle>().daytime){
                myCursor.setCursorDefault();
            } else {
                myCursor.setCursorCombat();
            }  
        } else {
             myCursor.setCursorDefault();
        }
    }
}
