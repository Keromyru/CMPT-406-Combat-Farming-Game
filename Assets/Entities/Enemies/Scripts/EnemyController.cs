using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IEnemyControl, ITakeDamage
{
    //Stats
    [Header("Enemy Stats")]
    private float enemyHealth = 1;
    private float enemyMoveSpeed;


    // Behaviors
    private EnemyOnAttackSO onAttackBehavior;
    private EnemyOnHitSO onHitBehavior;
    private EnemyOnDeathSO onDeathBehavior;
    // Controllers
    private AudioControllerSO audioController;
    private EnemyBehaviorSO myEnemyData;

    // Attack Data  
    private Coroutine attackRoutine; 
    private bool isWaiting; //Is in a state of waiting before it can attack again
    //

    ////////////////////////////////////////////////
    // LOGICS

    private void FixedUpdate() {
        //Death Check
        if (enemyHealth <= 0){ onDeath();}


        //Target Check  is not day, is not waiting, is able to attack, has a target, and the target is available
  /*      if (!isWaiting){
            onAttack(); //Does the Attack Action
            AttackTimer(); //starts the timer coroutine
        } 
  */      
    }
    ////////////////////////////////////////////////
    //Triggers
    ////////////////////////////////////////////////

    //Onhit is referenced by ITakeDamage interface
    public void onHit(float damage, GameObject source){
        //The Method Existing on the SO will trigger as well as pass final damage to the plant itself.
        //This can accomedate for any kind of damage negation that may be needed.
        //This also passes this game object so that the script may do whatever it needs with it, or it's position
        enemyHealth -= onHitBehavior.onHit(damage, source, this.gameObject); //Trigger onhit behaviors
    }

    public void onDeath(){
        //Triggers the attached Deal Trigger
        onDeathBehavior.onDeath(this.gameObject);
    }

    public void onAttack(){
      //  onAttackBehavior.OnAttack(onAttackBehavior.attackDamage,attackTarget,this.gameObject);
           
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
    #region Sets and Gets
    ////////////////////////////////////////////////
    //SETS 'n GETS
    public void setHealth(float newhealth) { enemyHealth = newhealth;}
    public float getHealth() {return enemyHealth;}
    public void setOnAttack( EnemyOnAttackSO newOnAttackBehavior) { onAttackBehavior = newOnAttackBehavior;}
    public void setOnHit( EnemyOnHitSO newOnHitBehavior) { onHitBehavior = newOnHitBehavior;}
    public void setOnDeath( EnemyOnDeathSO newOnDeathBehavior) { onDeathBehavior = newOnDeathBehavior;}
    public void setAudioController(AudioControllerSO newAudioController){ audioController = newAudioController;}

    public void setMyPlantData(PlantBehaviorSO newPlantData)
    {
        throw new System.NotImplementedException();
    }

    #endregion
    ////////////////////////////////////////////////
}
