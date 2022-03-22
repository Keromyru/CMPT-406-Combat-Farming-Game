using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

//TDK443 This is an extension Script for UI Elements Meant to fade in and out 
//All attactched Elements Should be ideal for anything Context Sensitive
//Made it because I'm burnt out and wanna do something fun.
//Can only do Prefab and entity management for so long
public class FadeOutEffect : MonoBehaviour {

    [Header("Fade Effect Options"),Space]
    [SerializeField] bool enableFadeEffect = true;
    [SerializeField] int fadeSpeed;
   
    private FadeObject[] myFadeObjects;

    public void FadeOut(){ 
        if(enableFadeEffect){StartCoroutine(FadeOutRoutine(fadeSpeed)); }
        }
    public void FadeIn(){
        if(enableFadeEffect){StartCoroutine(FadeInRoutine(fadeSpeed)); }
    }
   
    private void OnEnable() {
        //Creates a listing of every image on this the parent object
        //and saves it as images

        myFadeObjects = this.gameObject.GetComponentsInChildren<Image>().Select(image => new FadeObject(image)).ToArray();

    }
    // Peram: Requires Objects in the myFadeObjects 
    //Fades the Images on the game object out.
    private IEnumerator FadeOutRoutine( int fadeSpeed = 2) { 
        float fadeKey = 1;
        while (fadeKey > 0) {
            fadeKey -= Time.fixedDeltaTime;
            Array.ForEach(myFadeObjects, s => s.setAlpha(fadeKey));           
            yield return null;
        }
        yield return new WaitForEndOfFrame();
    }

    // Peram: Requires Objects in the myFadeObjects 
    //Fades the Images on the game object back in.
    private IEnumerator FadeInRoutine( int fadeSpeed = 2) { 
        float fadeKey = 0;
        while (fadeKey < 1) {
            fadeKey += Time.fixedDeltaTime;
            Array.ForEach(myFadeObjects, s => s.setAlpha(fadeKey));              
            yield return null;
        }
        yield return new WaitForEndOfFrame();
    }
}
