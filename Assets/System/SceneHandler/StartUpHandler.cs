using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
//TDK443
//This Script Exists To Deal With Additive Loading Inside Of The Unity Editor
//THIS HAS  BEEN ATTACHED TO GAMEHANDLER 
public class StartUpHandler : MonoBehaviour
{
    private Scene[] loadedScenes;
    void Start()
    {
        StartupCheck();
    }

    private void StartupCheck(){
        if (!ListOfLoadedScenes().Contains("System")){
            SceneManager.LoadScene("System", LoadSceneMode.Additive);
        }
    }

    //Creates an active directory of loaded  scenes
    private void GetLoadedScenes(){
    int countLoaded = SceneManager.sceneCount;
    loadedScenes = new Scene[countLoaded];

    for (int i = 0; i < countLoaded; i++)
    {
        loadedScenes[i] = SceneManager.GetSceneAt(i);
    }
    }

    //Creates a list of scene names of active scenes
    private List<String> ListOfLoadedScenes(){
        GetLoadedScenes();
        List<String> sceneNames = new List<String>();
        foreach (var Scene in loadedScenes)
        {
            sceneNames.Add(Scene.name);
        }
        return sceneNames;
    }


}
