using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;


public class script_DayNightTracker : MonoBehaviour {
	
	/* Simple day night tracker display script by Wilson */

	// This is for each digit to show on the tracker
	public Image digit1, digit2, digit3, digit4;
	
	// The icon for am or pm and day to night
	public Image amPm, day, night;
	
	// The day night icons
	public GameObject dayIcon, nightIcon;
	
	// This is the spriteAtlas to access for changes
	public SpriteAtlas spriteFonts;
	
	void Start() {
		
		// Starting day will be 00:00 and am
		digit1.sprite = spriteFonts.GetSprite( "0" );
		digit2.sprite = spriteFonts.GetSprite( "0" );
		digit3.sprite = spriteFonts.GetSprite( "0" );
		digit4.sprite = spriteFonts.GetSprite( "0" );
		
		//amPm.sprite = spriteFonts.GetSprite( "am" );
		dayIcon.SetActive( true );
		nightIcon.SetActive( false );
		
		
	}
	
    /* Changing the day icon to night, am to pm and vise-versa
		Input: None
		Return: None
	*/
	public void swapDayNight() {
		
		// Check what is currently set and adjust according
		// If day
		if ( dayIcon.activeSelf ) {
			
			// then swap around
			dayIcon.SetActive( false );
			nightIcon.SetActive( true );
			//amPm.sprite = spriteFonts.GetSprite( "pm" );
			
		} else {
			
			dayIcon.SetActive( true );
			nightIcon.SetActive( false );
			//amPm.sprite = spriteFonts.GetSprite( "am" );
			
		}
			
		
	}
	
	/* Updating and syncing the time in the day-night cycle
		Input: 
			currentHour - The int of current hour
			currentMinutes - The int of current minutes
		Return: None
	*/
	public void setTime( int currentHour, int currentMinutes ) {
		
		// Take values and split them
		int hour1 = currentHour / 10;	// X0:00
		int hour2 = currentHour % 10;	// 0X:00
		
		int min1 = currentMinutes / 10;	// 00:X0
		int min2 = currentMinutes % 10;	// 00:0X
		
		// Set the images
		digit1.sprite = spriteFonts.GetSprite( hour1.ToString() );
		digit2.sprite = spriteFonts.GetSprite( hour2.ToString() );
		digit3.sprite = spriteFonts.GetSprite( min1.ToString() );
		digit4.sprite = spriteFonts.GetSprite( min2.ToString() );
	
	}
	
    // Update is called once per frame
    void Update() {
        
    }
	
	
	
}
