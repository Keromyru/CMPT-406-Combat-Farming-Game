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
    public EnemyBehaviorSO myEnemyData;

    // Attack Data  
    private Coroutine attackRoutine; 
    private bool isWaiting; //Is in a state of waiting before it can attack again

    private GameObject attackTarget; // <--- STILL NEEDS TARGET LOGIC
    //

    ////////////////////////////////////////////////
    // LOGICS

    private void FixedUpdate() {
        //Death Check
        if (enemyHealth <= 0){ onDeath();}


        //This is the when it decides to attack... So whatever we need to determin if its target is applicable
        //There is no logic in this script to determin a target or to decide when to attack
        //  onAttackBehavior.attackRange is how you get it's attacking range
        // this is set up to attack when a target 
        if (!isWaiting  && //the attack timer has gone off
            attackTarget != null && //Does exist
            Vector2.Distance(this.transform.position, attackTarget.transform.position) < onAttackBehavior.attackRange){ //is within range
            onAttack(); //Does the Attack Action
            AttackTimer(); //starts the timer coroutine
        } 
        
    }
    ////////////////////////////////////////////////
    //Triggers
    ////////////////////////////////////////////////

    //Onhit is referenced by ITakeDamage interface
    public void onHit(float damage, GameObject source){
        //The Method Existing on the SO will trigger as well as pass final damage to the enemy itself.
        //This can accomedate for any kind of damage negation that may be needed.
        //This also passes this game object so that the script may do whatever it needs with it, or it's position
        if (myEnemyData.SoundOnHit != null && myEnemyData.SoundOnHit.Length < 1) {audioController.Play(myEnemyData.SoundOnHit);}
        enemyHealth -= onHitBehavior.onHit(damage, source, this.gameObject); //Trigger onhit behaviors
    }

    public void onDeath(){
        //Triggers the attached Deal Trigger
        //if (myEnemyData.SoundOnDeath != null) {audioController.Play(myEnemyData.SoundOnDeath);} //Play SoundOnDeath if the file has been declared
         if (myEnemyData.SoundOnDeath != null && myEnemyData.SoundOnDeath.Length < 1) {audioController.Play(myEnemyData.SoundOnDeath);}
        onDeathBehavior.onDeath(this.gameObject);
    }

    public void onAttack(){
      if (myEnemyData.SoundOnAttack != null) {audioController.Play(myEnemyData.SoundOnAttack);} //Play SoundOnAttack if the file has been declared  
    //   onAttackBehavior.OnAttack(onAttackBehavior.attackDamage,
    //   attackTarget,// <<<<<<<<<<<<<<<----------------------------------This is where on object that is to be attacked is chosen;
    //   this.gameObject);      
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

    public void setMyEnemyData(EnemyBehaviorSO newMyEnemyData) { myEnemyData = newMyEnemyData;}

    #endregion
    ////////////////////////////////////////////////
}
