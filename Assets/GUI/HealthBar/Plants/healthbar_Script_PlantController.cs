using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar_Script_PlantController : MonoBehaviour {
	
    /* Simple plant healthbar script by Wilson */
	
	// The fill bar to display remainder of health
	public Image fillBar;
	
	private float maxHealth; // The max health of the HUB
	private float currHealth; // Current health
	PlantController myPlant; 
	public Image healthBar;
	
	/* Initialize the values for the health bar */
	void Start() {
		myPlant = this.gameObject.transform.parent.GetComponent<PlantController>(); //Set Plant Data
		maxHealth = myPlant.getMaxHealth(); //Set Max Health
		setHealth(maxHealth);
	}
    
	/* Set current health of the HUB
		Input: health - The amount of health to set
		Return: None
	*/
	public void setHealth( float health ) {
		// Set the current health
		currHealth = health;
		fillBar.fillAmount = currHealth / maxHealth; // Normalize
	}
	
	/* Return the current health
		Input: None
		Return: currHealth - The current amount of health
	*/
	public float getHealth() {
		
		return currHealth;
		
	}

	public void updateHB(){
		setHealth(myPlant.getHealth());
	}

	public void setColor(Color newcolor){
		healthBar.color = newcolor;
	}
	
}
