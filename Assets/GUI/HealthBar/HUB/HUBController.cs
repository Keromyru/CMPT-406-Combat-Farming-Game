using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
	
	/* Simple Health Bar Script By Wilson */
	
	// The fill bar to display remainder of health
	public GameObject fillBar;
	
	public float maxHealth; // The max health of the HUB
	private float currHealth; // Current health
	
	/* Initialize the values for the health bar */
	void Start() {
		
		// Set the starting health to max
		currHealth = maxHealth;
		
	}
    
	/* Set current health of the HUB
		Input: health - The amount of health to set
		Return: None
	*/
	public void setHealth( int health ) {
		
		// Set the current health
		currHealth = health;
		
	}
	
	/* Return the current health
		Input: None
		Return: currHealth - The current amount of health
	*/
	public float getHealth() {
		
		return currHealth;
		
	}
	
}
