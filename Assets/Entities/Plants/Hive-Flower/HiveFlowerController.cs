using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class HiveFlowerController : PlantController {

    private UnityEngine.Experimental.Rendering.Universal.Light2D myLight; 

    [SerializeField] float lightOnDelay;
    [SerializeField] float lightOffDelay;   
    
    void Awake(){
        myLight = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
    }
    public override void newDay(){
        base.newDay();
        myLight.enabled = false;
        LampOff();
    }

    public override void newNight(){
        base.newNight();
        LampOn();
    }

    private void LampOn(){
        Debug.Log("Lamp on");
        StartCoroutine(LightOnRoutine(3,lightOnDelay));
    }
    private void LampOff(){
        StartCoroutine(LightOffRoutine(3,lightOffDelay));
    }


    private IEnumerator LightOffRoutine( int fadeSpeed = 2, float timer = 0) { 
        yield return new WaitForSeconds(timer);
        float fadeKey = 1;
        while (fadeKey > 0) {
            fadeKey -= Time.fixedDeltaTime*(1f/fadeSpeed);
            myLight.intensity = fadeKey;       
            yield return null;
        }
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator LightOnRoutine( int fadeSpeed = 2, float timer  = 0) { 
        yield return new WaitForSeconds(timer);
        float fadeKey = 0;
        while (fadeKey < 1) {
            fadeKey += Time.fixedDeltaTime*(1f/fadeSpeed);
            myLight.intensity = fadeKey;       
            yield return null;
        }
        yield return new WaitForEndOfFrame();
    }
}
