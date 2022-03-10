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

    // Attack Data  
    private Coroutine attackRoutine; 
    private bool isWaiting; //Is in a state of waiting before it can attack again


    
   



    //PLAYER LOGIC
    private void Awake() {
    myCamera = GameObject.Find("Main Camera");
    onAttackBehavior = myPlayerData.onAttackBehavior; //Set from Controller
    onHitBehavior = myPlayerData.onHitBehavior;// 
    
        
    }

    private void Update() {

        if ((playerInput.actions["PrimaryAction"].ReadValue<float>() > 0) && !isWaiting){ onAttack(); }
        Time.timeScale = 1;
        
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

    //SETS PLAYERINPUT FROM SYSTEM  with null catch
    void OnEnable() {SceneManager.sceneLoaded += OnSceneLoaded;}

    void OnDisable() {SceneManager.sceneLoaded -= OnSceneLoaded;}

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
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
