using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HubController : MonoBehaviour, ITakeDamage
{
    [Header("Hub Configeration")]
    public float hubMaxHealth;
	public GameObject transitionEffect;
    private Healthbar_HUBController myHealthbar;
    private float hubHealth = 1;

    [SerializeField] GameObject onDeathEffect;
    [SerializeField] AudioClipSO onHitSound;
    [SerializeField] AudioClipSO onDeathSound;
    [SerializeField] GameObject endGameObject;
    private bool gameOver = false;

    
    private void Start() {
        myHealthbar = GetComponentInChildren<Healthbar_HUBController>();
        hubHealth = hubMaxHealth;
    }

    void OnEnable() {DayNightCycle.isNowDay += newDay; }

    void OnDisable() { DayNightCycle.isNowDay -= newDay;} 

    public void onHit(float damage, GameObject source){
        hubHealth -= damage;
        if (onHitSound != null) { onHitSound.Play();} //Play onDeathSound
        myHealthbar.updateHB((int)hubHealth);
        if( hubHealth < 0){ onDeath();} //Check Death State
    }

    private void onDeath(){
        if (!gameOver){
            gameOver = true;
            if (onDeathEffect != null){ Instantiate(onDeathEffect, this.transform.position, this.transform.rotation); }
            if (onDeathSound != null) { onDeathSound.Play();} //Play onDeathSound
            Debug.Log("You have lost the game my dude");
            endGameObject.GetComponent<script_EndgameController>().setStats();
			// Play the transition effect
			playTransition();
			// Load the end screen
            SceneManager.LoadScene( "EndScreen", LoadSceneMode.Single );
        }

    }
	
	IEnumerator playTransition() {
		
		// Play animation and wait for delay
		transitionEffect.SetActive( true );
		transitionEffect.GetComponent<Animator>().Play("anim_TransitionEnter");
		
		yield return new WaitForSeconds( 1f );
		
	}	

    public void newDay(){
       hubHealth +=  (int)(hubMaxHealth*0.2f);
    }

    public void RestoreFull(){
        hubHealth = hubMaxHealth;
    }

    public void Restore(int amount){
        hubHealth += amount;
    }

    public int getHealth(){
        return (int)hubHealth;
    }
}

public static class MyHub{
    public static int Health(){ return GameObject.Find("Hub").GetComponent<HubController>().getHealth(); }
    public static void RestoreFull(){GameObject.Find("Hub").GetComponent<HubController>().RestoreFull(); } 
    public static void Restore(int amount){GameObject.Find("Hub").GetComponent<HubController>().Restore(amount); } 

}
