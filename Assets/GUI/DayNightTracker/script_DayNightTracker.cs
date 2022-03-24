using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;

public class script_DayNightTracker : MonoBehaviour {
	
	/* Simple day night tracker display script by Wilson */
	
	// This is for each digit to show on the tracker
	public TextMeshProUGUI digit;
	
	// The day night icons
	public GameObject dayIcon, nightIcon;
	
	void Start() {
		
		// Setup alignment of text
		digit.alignment = TextAlignmentOptions.Center;
		digit.fontSize = 300;
		
		// Starting day will be 00:00
		digit.text = "00:00";
		
		dayIcon.SetActive( true );
		nightIcon.SetActive( false );
		
		
	}
	
    /* Changing the day icon to night, am to pm and vise-versa
		Input: None
		Return: None
	*/
	public void swapDayNight( bool isDay ) {
		
		// Check what is currently set and adjust according
		// If day
		if ( isDay ) {
			
			// then swap around
			dayIcon.SetActive( true );
			nightIcon.SetActive( false );
			
		} else {
			
			dayIcon.SetActive( false );
			nightIcon.SetActive( true );
			
		}
			
		
	}
	
	/* Updating and syncing the time in the day-night cycle
		Input: 
			currentHour - The int of current hour
			currentMinutes - The int of current minutes
		Return: None
	*/
	public void setTime( int currentHour, int currentMinutes ) {
		
		digit.text = string.Format( "{0:00}:{1:00}", currentHour, currentMinutes );
	
	}
	
    // Update is called once per frame
    void Update() {
	
        
    }
	
}
