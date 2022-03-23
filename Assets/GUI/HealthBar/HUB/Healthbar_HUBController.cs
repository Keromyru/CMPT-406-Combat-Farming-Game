using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar_HUBController : FadeOutEffect {
	
	/* Simple Health Bar Script By Wilson */
	/* Updated With Effects and Event Interactions By TK */
	
	// The fill bar to display remainder of health
	[Space, Header("HUB healthbar Settings")]
	public Image fillBar;
	private float maxHealth; // The max health of the HUB
	private float currHealth; // Current health
	
	/* Initialize the values for the health bar */
	void Start() {
		HubController.HubAttacked += updateHB;
		// Set the starting health to max
		currHealth = maxHealth;
		fillBar.fillAmount = maxHealth / maxHealth; // Normalize value between 0.0 - 1.0
		fadeCheck();
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

	
	public void updateHB(int health){
		setHealth(health);
		fadeCheck();
	}

	/*  Sets the fade status based on need
		Input: None
		Return: nothing
	*/
	private void fadeCheck(){
		if(currHealth == maxHealth){ FadeOut();}
		else { FadeIn();}
	}
	
}
