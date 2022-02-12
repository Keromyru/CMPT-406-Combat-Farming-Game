using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//TDK443

public class SceneTrigger : MonoBehaviour
{
    [Header("This Script has configurations internally"), SerializeField]

    private bool enableTriggers = true;

    void OnEnable() { //Subscribe scene loaded trigger
        SceneManager.sceneLoaded += OnSceneLoaded;
    }   

    void OnDisable(){ //Remove Subscription of a loaded scene
        SceneManager.sceneLoaded -= OnSceneLoaded;
    } 

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        Debug.Log("OnSceneLoaded: " + scene.name);
            if (enableTriggers){
                ConfigureScene(scene.name);
                }
        }  

    //THIS IS WHERE YOU ADD ANYTHING THAT NEEDS TO BE TRIGGERED
    //WHEN A SCENE LOADS
     void ConfigureScene(string SceneName){
        switch(SceneName)
        {
        case "InGame":

            break;

        case "MainMenu":

            break;
        }
    }
}

