using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar_Script_PlantController : MonoBehaviour {
	
    /* Simple plant healthbar script by Wilson */
	
	// The fill bar to display remainder of health
	public Image fillBar;
	
	public float maxHealth; // The max health of the HUB
	private float currHealth; // Current health
	
	/* Initialize the values for the health bar */
	void Start() {
		
		// Set the starting health to max
		currHealth = maxHealth;
		fillBar.fillAmount = maxHealth / maxHealth; // Normalize value between 0.0 - 1.0
		
	}
    
	/* Set current health of the HUB
		Input: health - The amount of health to set
		Return: None
	*/
	public void setHealth( int health ) {
		
		// Set the current health
		currHealth = health;
		fillBar.fillAmount = health / maxHealth; // Normalize
		
	}
	
	/* Return the current health
		Input: None
		Return: currHealth - The current amount of health
	*/
	public float getHealth() {
		
		return currHealth;
		
	}
	
}
