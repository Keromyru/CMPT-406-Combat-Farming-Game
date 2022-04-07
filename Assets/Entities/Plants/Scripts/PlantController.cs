using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
//TDK443
//This is a base class that other controllers for each plant should inharit
// it should hold the base stats they all should impliment, and if all goes well their actions should be controllable by modular SO action objects **Shrugs** 
// Hopefully this doesn't become too convoluted
public class PlantController : MonoBehaviour, IPlantControl, ITakeDamage
{   
    #region Declarations
    //Interface exists in Assets/Entities/Plants/Scripts/IPlantControl.cs
    [Header("Do not set any values here")]
    [SerializeField]private float health = 1;
    [SerializeField]private float energy;
    [SerializeField] private int growAge;
    [SerializeField]private Vector3 location; //Location is just where it lives, this is stored for easy Save Retrieval
    private bool dayTime = false;  
    [SerializeField] private bool isReady = false;

    private PlantBehaviorSO myPlantData; //References the plants Entry in the Database
    private PlantDatabaseSO myPlantSpawner; //References the spawner to it can call it's decendants
    private AudioControllerSO audioController;

    // Behaviors
    private PlantOnHitSO onHitBehavior;
    private PlantOnAttackSO onAttackBehavior;
    private PlantOnDeathSO onDeathBehavior;
    private PlantOnHarvestSO onHarvestBehavior;

    // Attack Data  
    private Coroutine attackRoutine; 
    private bool isWaiting; //Is in a state of waiting before it can attack again
    [SerializeField] List<GameObject> targets;
    // Other References
    private healthbar_Script_PlantController myHealthBar;
    private AudioSource mySource;

    [Header("MiniMap Icons")]

    [SerializeField] GameObject plantIcon;
    [SerializeField] GameObject dangerIcon;


    #endregion
    ////////////////////////////////////////////////
    // PLANT LOGICS

    //Sets the Healthbar controller
    private void Start() { 
            if(this.gameObject.GetComponentInChildren<healthbar_Script_PlantController>() != null){
                myHealthBar = this.gameObject.GetComponentInChildren<healthbar_Script_PlantController>();
            }
            mySource = this.gameObject.GetComponent<AudioSource>();
            dangerIcon.SetActive(false);
        }

    // private void Update() { //Triggers NewDAY for testing
    //         if (Input.GetKeyDown("space")){
    //         newDay();
    //         Debug.Log("New Day "+myPlantData.name+" is "+growAge+" days old      "+(growAge-myPlantData.DaysUntilHarvest));
    //     }
    // }

    private void FixedUpdate() {
        //Target Check  is not day, is not waiting, is able to attack, has a target, and the target is available
        if (!dayTime && !isReady && myPlantData.canAttack && !isWaiting && targets.Count > 0){
            targets = targets.OrderBy(t => distance(t)).ToList();
            if (distance(targets[0]) <= onAttackBehavior.attackRange){
                onAttack(); //Does the Attack Action
                AttackTimer(); //starts the timer coroutine
            }

        } 
    }

    //Plant Actions
    //Basically when things enter it's zone it'll add it to a tracking list, and if it leaves it'll remove it.
    private void OnTriggerEnter2D(Collider2D entity) {
        if (entity.tag == "Enemy" && myPlantData.canAttack) {
            targets.Add(entity.gameObject);     
        }
    }

    private void OnTriggerExit2D(Collider2D entity) {
        if (entity.tag == "Enemy"  && myPlantData.canAttack){
            targets.Remove(entity.gameObject); //Remove That Object From Its Attack List
        }
    }
    void OnEnable() {
        DayNightCycle.isNowDay += newDay;
        DayNightCycle.isNowNight += newNight;
    } //Subscribe to on Scene Loaded Event

    void OnDisable() {
        DayNightCycle.isNowDay -= newDay;
        DayNightCycle.isNowNight -= newNight;
    } //unsubscribe to on Scene Loaded Event

    private float distance(GameObject baddy){
        return Vector3.Distance(baddy.transform.position, gameObject.transform.position);
    }

    public bool waterPlant(float amount){
        health += amount;
        //Catch to prevent overfilling
        if (health > myPlantData.plantMaxHealth) { health = myPlantData.plantMaxHealth;}
        if(myHealthBar != null) {myHealthBar.updateHB();} //update Healthbar  
        //Returns true if plant energy is now ful
        if (health >= myPlantData.plantMaxHealth){
            plantIcon.SetActive(true);
            dangerIcon.SetActive(false);
            return true;
        }
        else { return false;}
    }

    //Replaces The Current Plant With Its next phase.
    //Going to be checked on the "Newday" trigger
    private void nextGrowthPhase(){
        if (myPlantData.soundGrowth != null) {audioController.Play(myPlantData.soundGrowth, mySource);} //Play soundGrowth if the file has been declared
        //Spawns the next plant in line
        if(myPlantData.GrowPhaseEffect != null){Instantiate(myPlantData.GrowPhaseEffect,this.transform.position,Quaternion.identity);}
        myPlantData.nextPhase.spawnNextPlant(
            myPlantData.nextPhase.name,
            this.location,
            myPlantData.plantMaxHealth - health,
            myPlantData.plantMaxEnergy - energy);
        Destroy(this.gameObject);
    }

    private void checkGrowthPhase(){
        if(myPlantData.nextPhase != null && 
            growAge >= myPlantData.matureAge ) {
            nextGrowthPhase();
        }
    }
    

    ////////////////////////////////////////////////
    //Triggers
    ////////////////////////////////////////////////
    //This should be a call that would be triggered by the time control system as an event or an interated list of
    //the IPlantControl interface
    public virtual void newDay(){
        growAge++; 
        Debug.Log(growAge);
        dayTime = true;
        targets.Clear(); //Clear Attack List
        checkGrowthPhase();
        if (myPlantData.harvestable && growAge >= myPlantData.DaysUntilHarvest){
            isReady = true;
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(.5f, .5f, .5f);
        }
        if(isReady){ onHarvestReady();}
        if(myHealthBar != null) {myHealthBar.updateHB();} //update Healthbar
        
    }

    public virtual void newNight(){
        dayTime = false;
        checkGrowthPhase();
    }

    //Onhit is referenced by ITakeDamage interface
    public void onHit(float damage, GameObject source){
        //The Method Existing on the SO will trigger as well as pass final damage to the plant itself.
        //This can accomedate for any kind of damage negation that may be needed.
        //This also passes this game object so that the script may do whatever it needs with it, or it's position
        health -= onHitBehavior.onHit(damage, source, this.gameObject); //Trigger onhit behaviors
        if (myPlantData.soundHurt != null) {audioController.Play(myPlantData.soundHurt, mySource);}
        if (GetComponent<FlashEffect>() != null){GetComponent<FlashEffect>().flash();} //Flash Effect On Hit
        if(myHealthBar != null) {myHealthBar.updateHB();} //update Healthbar 
        plantIcon.SetActive(false);
        dangerIcon.SetActive(true); 
        if (health <= 0){ onDeath();} //Death Check
    }

    public void onDeath(){
        //Triggers the attached Deal Trigger
        if (myPlantData.soundDeath != null) {audioController.Play(myPlantData.soundDeath, mySource);}
        onDeathBehavior.onDeath(this.gameObject);
    }

    public virtual void onAttack(){
        onAttackBehavior.OnAttack(onAttackBehavior.attackDamage,targets,this.gameObject);
        //Attack Sound
        if (myPlantData.soundAttack != null) { audioController.Play(myPlantData.soundAttack, mySource);}
    }

    public bool onHarvest(){
        if(myPlantData.harvestable && isReady){
            if (myPlantData.soundHarvested != null) {audioController.Play(myPlantData.soundHarvested, mySource);}
            onHarvestBehavior.OnHarvest(this.gameObject);
            growAge = 0;
            isReady = false;
            return true;
        }
        else {
            return false;
        }
    }

    public void onHarvestReady(){
        health -= ((growAge - myPlantData.DaysUntilHarvest)*myPlantData.plantMaxHealth*0.5f);
        if (health <= 0){ onDeath();} //Death Check }    
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
        yield return new WaitForSeconds(onAttackBehavior.attackRate);
        // After the pause, swap back to the original material.
        isWaiting = false;
        // Set the routine to null, signaling that it's finished.
        attackRoutine = null;
    }

    ////////////////////////////////////////////////
    #region Types
    //Types
    public enum type
    {
        defense,
        produce,
        utility
    }
    #endregion

    #region Sets and Gets
    ////////////////////////////////////////////////
    //SETS 'n GETS
    public void setOnHit(PlantOnHitSO newOnHit){onHitBehavior = newOnHit; }
    public void setOnDeath(PlantOnDeathSO newOnDeath){ onDeathBehavior = newOnDeath; }
    public void setOnAttack(PlantOnAttackSO newOnAttack){ onAttackBehavior = newOnAttack; }
    public void setOnHarvest(PlantOnHarvestSO newOnHarvest){ onHarvestBehavior = newOnHarvest; }
    public void setAudioController( AudioControllerSO newAudioController) { audioController = newAudioController;}
    public float getRemaining() { return myPlantData.plantMaxHealth - health; }
   
    public void setMyPlantData(PlantBehaviorSO newPlantData) {myPlantData = newPlantData; }
    public void setMyPlantSpawner(PlantDatabaseSO newPlantSpawner) {myPlantSpawner = newPlantSpawner; }
    public void setLocation(Vector3 newLocation){ location = newLocation; }
    public Vector3 getLocation(){ return location; }

    public void setGrowAge(int newGrowAge){ growAge = newGrowAge; }
    public int getGrowAge(){ return growAge; }

    public bool HarvestReady(){ return isReady; }
    public void setHealth(float newHealth){ health = newHealth;}
    public float getHealth(){return health;}
    public float getMaxHealth(){return myPlantData.plantMaxHealth;}
    public float getMaxEnergy(){return myPlantData.plantMaxEnergy;}
    public void setEnergy(float newEnergy){ energy = newEnergy;}
    public float getEnergy(){ return energy;}

    #endregion Sets and Gets
}

