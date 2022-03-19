using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSceneHandler
{
    public static void playGame(){
       SceneHandlerSO handler = ScriptableObject.FindObjectOfType<SceneHandlerSO>(); 
       handler.LoadScenario("inGame");
       handler.UnloadMenus();
    }

    public static void LoadMainMenu(){
        SceneHandlerSO handler = ScriptableObject.FindObjectOfType<SceneHandlerSO>(); 
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }
}
