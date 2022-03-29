using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class HiveFlowerController : PlantController {

    private UnityEngine.Experimental.Rendering.Universal.Light2D mylight; 
    
    void Start(){
        mylight = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        Debug.Log(mylight);
    }
    public override void newDay(){
        base.newDay();
        mylight.enabled = false;
    }

    public override void newNight(){
        base.newNight();
        StartCoroutine(turnOnLight());
    }

    IEnumerator turnOnLight(){
       yield return new WaitForSeconds(2f);
        mylight.enabled = true;
    }
}
