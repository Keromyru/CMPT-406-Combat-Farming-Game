using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
//TDK443
public class GameSceneHandler: MonoBehaviour
{
    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;   
    }
    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    public static void PlayGame(){
       SceneHandlerSO handler = ScriptableObject.FindObjectOfType<SceneHandlerSO>(); 
       handler.LoadScenario("inGame");
       handler.UnloadMenus();
    }

    public static void LoadMainMenu(){
        SceneHandlerSO handler = ScriptableObject.FindObjectOfType<SceneHandlerSO>(); 
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        //Array.ForEach(GetLoadedScenes(), m => Debug.Log(m)); //Lists All Scenes
        if(GetLoadedScenes().Contains("MainMenu")){
            //AudioHandler.StartPlayList("Menus");
            MusicPlayer.StartPlayList("Menus");
        }
        else if(GetLoadedScenes().Contains("InGame")){
            MusicPlayer.StartPlayList("DayCycle");
        }     
    }

    //Updates an Active directory of loaded  scenes
    public String[] GetLoadedScenes(){
        Scene[] loadedScenes = new Scene[SceneManager.sceneCount];
        for (int i = 0; i < SceneManager.sceneCount; i++){
            loadedScenes[i] = SceneManager.GetSceneAt(i);
        }
        
        return loadedScenes.Select(scene => scene.name).ToArray();
    }

}
