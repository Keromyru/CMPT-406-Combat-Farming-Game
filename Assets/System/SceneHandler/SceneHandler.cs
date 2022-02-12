using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    //TRIGGER POINT FOR ANYTHING THAT NEEDS TO BE CHECKED
    //OR HAPPEN BEFORE A SCENE IS LOADED
    private void OnSceneLoad(){
    
    }
    //SET SCENE
    public void SetScene(string SceneName){
        OnSceneLoad();
        SceneManager.LoadScene(SceneName);
    }

    

//GET SCENE NAME
    public string GetScene(){
        return SceneManager.GetActiveScene().name;
    }

    

}
