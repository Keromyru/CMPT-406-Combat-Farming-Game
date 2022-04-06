using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class EnemyController : MonoBehaviour, IEnemyControl, ITakeDamage
{
    //Stats
    [Header("Don't Set These")]
    private float enemyHealth = 1;
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
    public GameObject attackTarget; // <--- STILL NEEDS TARGET LOGIC
    // Animations~
    public Animator myAnimation;
	public Rigidbody2D enemyRb;
    private SpriteRenderer myRenderer;
    private bool itsMoving;
    private bool toTransform;

    // slow stuff

    private float slowTimer;
    public float slowMulti = 1; 
    public List<int> slowTickTimers = new List<int>();
    


    ////////////////////////////////////////////////
    // LOGICS

    private void Start() {
        myRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }
    private void FixedUpdate() {
        //Death Check
        if (enemyHealth <= 0){ onDeath();}

        // This is the when it decides to attack... So whatever we need to determin if its target is applicable
        // There is no logic in this script to determin a target or to decide when to attack
        //  onAttackBehavior.attackRange is how you get it's attacking range
        // this is set up to attack when a target 
        if (!isWaiting  && attackTarget != null ) {
            if( Vector2.Distance(this.transform.position, attackTarget.transform.position) < myEnemyData.attackRange ||
            (attackTarget.tag == "Structure" && attackTarget.GetComponents<Collider2D>().Any(s => (Vector2.Distance(this.transform.position, s.ClosestPoint(this.transform.position)) < myEnemyData.attackRange )))) {
                onAttack(); //Does the Attack Action
                AttackTimer(); //starts the timer coroutine
            }
        }
        //SLOW CHECK
        if (slowTimer > 0){
            slowTimer = slowTimer - Time.deltaTime;
            myRenderer.color = new Color(255,0,255,255);
        } else{
            myRenderer.color = new Color(255,255,255,255);
        }
    }

    ////////////////////////////////////////////////
    //Triggers
    ////////////////////////////////////////////////

    //Onhit is referenced by ITakeDamage interface
    public virtual void onHit(float damage, GameObject source){
        //The Method Existing on the SO will trigger as well as pass final damage to the enemy itself.
        //This can accomedate for any kind of damage negation that may be needed.
        //This also passes this game object so that the script may do whatever it needs with it, or it's position
        if (myEnemyData.SoundOnHit != null && myEnemyData.SoundOnHit.Length < 1) {audioController.Play(myEnemyData.SoundOnHit);}
        enemyHealth -= onHitBehavior.onHit(damage, source, this.gameObject); //Trigger onhit behaviors
    }

    public virtual void onDeath(){
        //Triggers the attached Deal Trigger
        //if (myEnemyData.SoundOnDeath != null) {audioController.Play(myEnemyData.SoundOnDeath);} //Play SoundOnDeath if the file has been declared
         if (myEnemyData.SoundOnDeath != null && myEnemyData.SoundOnDeath.Length < 1) {audioController.Play(myEnemyData.SoundOnDeath);}
        onDeathBehavior.onDeath(this.gameObject);
    }

    public virtual void onAttack(){
        if (myEnemyData.SoundOnAttack != null && myEnemyData.SoundOnAttack.Length > 0) {audioController.Play(myEnemyData.SoundOnAttack);} //Play SoundOnAttack if the file has been declared  
        Debug.Log(this.gameObject.name+" is doing an attack!");
      onAttackBehavior.OnAttack(myEnemyData.attackDamage,
      attackTarget,// <<<<<<<<<<<<<<<----------------------------------This is where on object that is to be attacked is chosen;
      this.gameObject);      
    }
    
    // Status Effect 
    // when enemy enters the area, 
    // it goes slower


    public void ApplySlow(float newSlowMulti, float duration)
    {
        // this.slowTimer = slowTimer;
        // if(slowTimer <= 0)
        // {
        //     slowTimer.Add(ticks);
        //     StartCoroutine(Slow());
        // }
        // else
        // {
        //     slowTimer.Add(ticks);
        // }
        slowTimer = duration;
        slowMulti = newSlowMulti;
    }

    // IEnumerator Slow(){
    //     while(slowTimer.Count > 0){
    //         for(int i = 0; i < slowTickTimers.Count; i++)
    //         {
    //             slowTickTimers[i]--;
    //         }
    //         slowTickTimers.RemoveAll(i >= i == 0);

    //     }
    // }


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
        yield return new WaitForSeconds(myEnemyData.attackRate*(1/slowMulti));
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
    public void setTarget(GameObject myTarget){ attackTarget = myTarget;}

    #endregion
    ////////////////////////////////////////////////
}
