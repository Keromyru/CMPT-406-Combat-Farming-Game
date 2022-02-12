using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameScene 
{
    public static void SetScene(string name){
        GameObject.Find("SceneHandler").GetComponent<SceneHandler>().SetScene(name); 
    }

    public static string GetScene(){
        return GameObject.Find("SceneHandler").GetComponent<SceneHandler>().GetScene(); 
    }
}
