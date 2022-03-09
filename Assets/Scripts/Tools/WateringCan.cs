using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
	
	public int pourAmount; // Amount of times player can pour before refilling
	public int currAmount; // The current amount
	
	public GameObject waterCan; // The watering can object
	
	private Collision2D collider; // The tile the player is standing on
	
    // Update is called once per frame
    void Update() {
		// When user clicks fire and watering can is active
		if ( Input.GetButtonDown( "Fire1" ) && waterCan.activeSelf ) {
			// True, check if we can water tile
			if ( canPour() ) {
				// Pour it
				pourWater();
				
			} else {
				// Send message about the watering can being empty
				// TODO: Create message
				
			}
			
		}
	
    }
	
	/* Store the collision details
		Input: collision - The object collided
		Returns: None
	*/
	void OnCollisionEnter2D( Collision2D collision ) {
		// Only store collision with hex grid
		// TODO: Add tag for hex tile
		if ( collision.gameObject.tag == "HexGrid" ) {
			
			collider = collision;
		}
		
	}
		
	/* Refill watering can fully
		Input: None
		Returns: None
	*/
	public void reFill() {
		
		currAmount = pourAmount;
		
	}
	
	/* Pour water on tile
		Input: tile - Tile which player is standing on
		Returns: None
	*/
	private void pourWater() {
		// Check if the collider is empty
		if ( collider != null ) {
			// Set the tile to be wet
			// TODO: Create method to set tile or plant status
			
			// Subtract from pour amount
			currAmount -= 1;
		
		}
		
	}
	
	/* Check if the watering can is empty
		Input: None
		Returns: boolean - true if can pour else false
	*/
	private bool canPour() {
		
		if ( pourAmount > 0 ) { return true; }
		else return false;
		
	}
		
}
