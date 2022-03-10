using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, IPlayerControl, ITakeDamage
{
    private float health;

    [SerializeField] PlayerBevahviorSO myPlayerData;
    private PlayerOnAttackSO onAttackBehavior;
    private PlayerOnHitSO onHitBehavior;
    private GameObject myCamera;
    private PlayerInput playerInput;  // The inputs from the InputSystem
    private Rigidbody2D playerRB;     
    // Attack Data  
    private Coroutine attackRoutine; 
    private bool isWaiting; //Is in a state of waiting before it can attack again


    
   
    void Start(){GameCamera.SetTarget(this.gameObject);}//Sets the player as the camera focus

    //PLAYER LOGIC
    private void Awake() { 
    myCamera = GameObject.Find("Main Camera"); //Find and set camera
    playerRB = this.GetComponent<Rigidbody2D>(); //Set Rigid Body Shortcut for Blakes Code
    onAttackBehavior = myPlayerData.onAttackBehavior; //Set onAttackBehavior
    onHitBehavior = myPlayerData.onHitBehavior; //Set onHitBehavior
    
        
    }

    private void Update() {
        //Checks if the mouse click is down, and if the reset timer isn't set
        //The behavior of this will be shots so long as  the left-click is held
        if ((playerInput.actions["PrimaryAction"].ReadValue<float>() > 0) && !isWaiting){ onAttack(); }
        Time.timeScale = 1; //GLITCH, SOMETHING IS PAUSING THE GAME TIMESCALE????
        
    }

    private void FixedUpdate() {
        // Written by Blake Williams
        // Gets the movement input and applies a constant velocity to the player
        Vector2 inputVector = playerInput.actions["PlayerMovement"].ReadValue<Vector2>();
        playerRB.MovePosition(playerRB.position + new Vector2(inputVector.x, inputVector.y) * myPlayerData.moveRate);

        
    }


    //Pointer Location Extracted from Blakes Code
    private Vector2 pointerLocation(){
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = -1000;
        Vector3 MousePosition = myCamera.GetComponent<Camera>().ScreenToWorldPoint(mousePos);
        MousePosition.y += 523f;
        MousePosition.z = 0f;
   
        return MousePosition;
    }

    public void resetHealth(){
        health = myPlayerData.maxHealth;
    }

    
    void OnEnable() {SceneManager.sceneLoaded += OnSceneLoaded;} //Subscribe to on Scene Loaded Event

    void OnDisable() {SceneManager.sceneLoaded -= OnSceneLoaded;} //unsubscribe to on Scene Loaded Event

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) { //If not null, add player input to "playerInput"
        if (GameObject.Find("InputHandler") != null) { playerInput = GameObject.Find("InputHandler").GetComponent<PlayerInput>();}
        }

    //TRIGGERS
    public void onHit(float damage, GameObject source)
    {    //if it's a baddy, take the damage
         if (source.tag == "Enemy"){ health -= onHitBehavior.onHit(damage,source, this.gameObject);}
    }

    public void newDay(){ //On a new day
    resetHealth();
    }
   

    public void newNight(){
    

    }

    public void onDeath(){
       

    }
    
    //Should not be triggered 
    public void onAttack(){
        if (!isWaiting){
            onAttackBehavior.OnAttack(
                onAttackBehavior.damage,
                pointerLocation(),
                this.gameObject
            );

            AttackTimer();//START TIMER
        }


           
    }



    ////////////////////////////////////////////////
    // Attack Interval Corutine

    //starts attack Timer
     public void AttackTimer(){
        if (attackRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple AttackRoutine at the same time would cause bugs.
            StopCoroutine(attackRoutine);
        }
        // Start the Coroutine, and store the reference for it.
        attackRoutine = StartCoroutine(AttackRoutine());            
     }

    private IEnumerator AttackRoutine(){
        //toggles on the screen shake function
        isWaiting = true;
        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(onAttackBehavior.fireRate);
        // After the pause, swap back to the original material.
        isWaiting = false;
        // Set the routine to null, signaling that it's finished.
        attackRoutine = null;
    }
    ////////////////////////////////////////////////

}
