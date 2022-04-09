using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

///////////////////////////////////////////////////////////////
///    Scene Handler
///////////////////////////////////////////////////////////////

[CreateAssetMenu(fileName = "SceneHandler", menuName = "Scene Data/Scene Handler")]
public class SceneHandlerSO : ScriptableObject
{
    public List<ScenarioSO> scenarios = new List<ScenarioSO>();
    public List<MenuSO> menus = new List<MenuSO>();
    public List<GuiSO> guis = new List<GuiSO>();
    private Scene[] loadedScenes;

///////////////////////////////////////////////////////////////
// SCENE LOADED EVENT SUBSCRIPTION
    void OnEnable() {SceneManager.sceneLoaded += OnSceneLoaded;}
    void OnDisable(){SceneManager.sceneLoaded -= OnSceneLoaded;} 
///////////////////////////////////////////////////////////////
// Scene Loads

/*
* Scenarios
*/
    public void LoadScenario(string Name){
        foreach (ScenarioSO scene in scenarios)
        {
            if (scene.sceneName.Contains(Name)){
            //LOAD THIS WITH YOUR OPTIONS
           
            SceneManager.LoadScene(Name, LoadSceneMode.Additive);
            return;
            }
        }
        Debug.LogWarning("Scene : "+ name + " not found");
        return;
    }
/*
* MENUS
*/
    public void LoadMenu(string Name)
    {
        foreach (MenuSO scene in menus)
        {
            if (scene.sceneName.Contains(Name)){
            //LOAD THIS WITH YOUR OPTIONS
           
            SceneManager.LoadScene(Name, LoadSceneMode.Additive);
            return;
            }
        }
        Debug.LogWarning("Scene : "+ name + " not found");
        return;
    }

/*
* GUI
*/
    public void LoadGUI(string Name)
    {
        foreach (GuiSO scene in guis)
        {
            if (scene.sceneName.Contains(Name)){
            //LOAD THIS WITH YOUR OPTIONS


            SceneManager.LoadScene(Name, LoadSceneMode.Additive);
            Debug.Log("GUI Loaded");
            return;
            }
        }
        Debug.LogWarning("Scene : "+ name + " not found");
        return;
    }
///////////////////////////////////////////////////////////////

    public void UnloadMenus(){
        foreach (MenuSO scene in menus){
            SceneManager.UnloadSceneAsync(scene.sceneName);
        }
    }
///////////////////////////////////////////////////////////////
// 
    public void LoadMainMenu(){
        LoadMenu("MainMenu");
    }

    public void SwapMenu(String menuName){
        UnloadMenus();
        LoadMenu("menuName");
    }

    public void NewScenario(){

    }

    public void LoadScenario(){

    }




///////////////////////////////////////////////////////////////
// 
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        //Makes sure that system is loaded if it's not already loaded
        StartupCheck();
        
    }  

    private void StartupCheck(){
        if (!ListOfLoadedScenes().Contains("System")){
            SceneManager.LoadScene("System", LoadSceneMode.Additive);
        }
        // if (!ListOfLoadedScenes().Contains("EndGame")){
        //     SceneManager.LoadScene("EndGame", LoadSceneMode.Additive);
        // }
    }


    //Updates an Active directory of loaded  scenes
    public void GetLoadedScenes(){
        int countLoaded = SceneManager.sceneCount;
        loadedScenes = new Scene[countLoaded];
 
        for (int i = 0; i < countLoaded; i++)
        {
            loadedScenes[i] = SceneManager.GetSceneAt(i);
        }
    }
    //Creates a list of scene names of active scenes
    public List<String> ListOfLoadedScenes(){
        GetLoadedScenes();
        List<String> sceneNames = new List<String>();
        foreach (var Scene in loadedScenes)
        {
            sceneNames.Add(Scene.name);
        }
        return sceneNames;
    }

}

