using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//TDK443
//This is a base class that other controllers for each plant should inharit
// it should hold the base stats they all should impliment, and if all goes well their actions should be controllable by modular SO action objects **Shrugs** 
// Hopefully this doesn't become too convoluted
public class PlantController : MonoBehaviour, IPlantControl, ITakeDamage
{   
    #region Declarations
    //Interface exists in Assets/Entities/Plants/Scripts/IPlantControl.cs
    [Header("Do not set any values here")]
    [SerializeField]private float health;
    [SerializeField]private float maxHealth;
    [SerializeField]private float energy;
    [SerializeField]private float maxEnergy;
    [SerializeField] private int growAge;
    [SerializeField] bool canAttack;
    [SerializeField] float attackRate; //This is an interval
    [SerializeField] float attackRange;
    [SerializeField] float attackDamage;

    
    [SerializeField] bool harvestable;
    [SerializeField] bool canDieOfOldAge;
    [SerializeField] int matureAge;
    [SerializeField] int harvestCycle;
    [SerializeField] int deathAge;


    private bool dayTime = true;
    private Vector2 location; //Location is just where it lives, this is stored for easy Save Retrieval
    private type plantType; 
    // Behaviors
    private PlantOnHitSO onHitBehavior;
    private PlantOnAttackSO OnAttackBehavior;
    private PlantOnDeathSO OnDeathBehavior;

    // Attack Data  
    private Coroutine attackRoutine; 
    private bool isWaiting; //Is in a state of waiting before it can attack again

    private GameObject  attackTarget;
    public List<GameObject> targets;

    #endregion
    ////////////////////////////////////////////////
    // PLANT LOGICS
    private void FixedUpdate() {
        //Death Check
        if (health <= 0){ onDeath();}

        //Target Check  is not day, is not waiting, is able to attack, has a target, and the target is available
        if (canAttack && !dayTime && !isWaiting &&  targets.Count > 0 && CheckTarget()){
            onAttack(); //Does the Attack Action
            AttackTimer(); //starts the timer coroutine
        }
        

        
    }

    //Plant Targeting 
    //Basically when things enter it's zone it'll add it to a tracking list, and if it leaves it'll remove it.
    private void OnTriggerEnter2D(Collider2D entity) {
        if (entity.tag == "Enemy" && attackTarget == null && canAttack) {
            targets.Add(entity.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D entity) {
        if (entity.tag == "Enemy" && attackTarget == null){
            targets.Remove(entity.gameObject);
        }
    }

    private bool CheckTarget(){ //If the target doesn't exist, or it's out of range, or it's daytime;

        if( (attackTarget == null || Vector3.Distance(attackTarget.transform.position, location) > attackRange) && !dayTime ){
            attackTarget = null;
            return SetTarget();
        }
        else
        return false;
    }
    private bool SetTarget(){
        float tDist = 1000; //Starts with an absurd distance
        GameObject potentialTarget = null; //Sets a place holder
        foreach (GameObject baddy in targets){ //checks all it's targets for a new option
            float distance = (Vector3.Distance(baddy.transform.position, gameObject.transform.position));
            if (distance < tDist){ //If this distance is better than any other 
                tDist = distance;
                potentialTarget = baddy; // Sets the potential target
            } 
        }
        attackTarget = potentialTarget;
        if (potentialTarget == null){ // Sets the returns if there's no good targets
            return false;
        }
        else {
            return true;
        }
    }





    ////////////////////////////////////////////////
    //Triggers
    ////////////////////////////////////////////////
    //This should be a call that would be triggered by the time control system as an event or an interated list of
    //the IPlantControl interface
    public void newDay(){
        growAge++; 
        dayTime = true;
        targets.Clear(); //Clear Attack List


    }

    public void newNight(){
        dayTime = false;

    }

    //Onhit is referenced by ITakeDamage interface
    public void onHit(float damage, GameObject source){
        //The Method Existing on the SO will trigger as well as pass final damage to the plant itself.
        //This can accomedate for any kind of damage negation that may be needed.
        //This also passes this game object so that the script may do whatever it needs with it, or it's position
        health -= onHitBehavior.onHit(damage, source, this.gameObject); //Trigger onhit behaviors
    }

    public void onDeath(){
        //Triggers the attached Deal Trigger
        OnDeathBehavior.onDeath(this.gameObject);
    }

    public void onAttack(){
        OnAttackBehavior.OnAttack(attackDamage,attackTarget,this.gameObject);
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
        yield return new WaitForSeconds(attackRate);
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
    public void setOnDeath(PlantOnDeathSO newOnDeath){ OnDeathBehavior = newOnDeath; }
    public void setOnAttack(PlantOnAttackSO newOnAttack){ OnAttackBehavior = newOnAttack; }

    public void setLocation(Vector2 newLocation){ location = newLocation; }
    public Vector2 getLocation(){ return location; }

    public void setGrowAge(int newGrowAge){ growAge = newGrowAge; }
    public int getGrowAge(){ return growAge; }

    public void setHealth(float newHealth){ health = newHealth;}
    public float getHealth(){return health;}

    public void setMaxHealth(float newHealth){ maxHealth = newHealth;}
    public float getMaxHealth(){return maxHealth;}

    public void setEnergy(float newEnergy){ energy = newEnergy;}
    public float getEnergy(){ return energy;}

    public void setMaxEnergy(float newEnergy){ maxEnergy = newEnergy;}
    public float getMaxEnergy(){ return maxEnergy;}

    public void setAttackRange(float newRange){ attackRange = newRange;}
    public float getAttackRange(){ return attackRange;}

    public void setAttackDamage(float newDamage){ attackDamage = newDamage;}
    public float getAttackDamage(){ return attackDamage;}

    public void setAttackRate(float newRate){ attackRate = newRate;}
    public float getAttackRate(){ return attackRate;}
    public void setType(int newType){ plantType = (type)Enum.ToObject(typeof(type), newType); }
    public int getType(){ return (int)plantType; }

    public void setHarvestable(bool newHarvastable){ harvestable = newHarvastable;}
    public bool getHarvestable(){ return harvestable;}
    #endregion Sets and Gets

}
