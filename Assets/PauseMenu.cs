using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class PauseMenu : MonoBehaviour
{
    // The inputs from the input system
    private PlayerInput playerInput;

    public GameObject inventoryui;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI; 

    // Update is called once per frame
    // pause is set to the escape key
    // hit escape key again to resume
    void Update() {
    if(Input.GetKeyDown(KeyCode.Escape))
    {
        if(GameIsPaused) {
            Resume();
        }
        else{
            Pause();
        }
    }   
}

//resume state
public void Resume(){
    pauseMenuUI.SetActive(false);
    Time.timeScale =  1f;
    GameIsPaused = false;
    if(inventoryui.active){
        playerInput.SwitchCurrentActionMap("InputUI");
    }
    else{
        playerInput.SwitchCurrentActionMap("InputPlayer");
    }
}

//paused state
void Pause(){
    pauseMenuUI.SetActive(true);
    Time.timeScale =  0f;
    GameIsPaused = true;
}

//load the main menu
public void LoadMenu(){
    SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

}

//quit the game
public void QuitGame(){
    Application.Quit();
}

//save game in current state
public void SaveGame(){
    //TODO
    
}

// Next three functions are to get the Player Input from the other scene
void OnEnable()
{
    SceneManager.sceneLoaded += OnSceneLoaded;
}

void OnDisable()
{
    SceneManager.sceneLoaded -= OnSceneLoaded;
}

void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    if (GameObject.Find("InputHandler") != null)
    {
        playerInput = GameObject.Find("InputHandler").GetComponent<PlayerInput>();
    }
}

}



