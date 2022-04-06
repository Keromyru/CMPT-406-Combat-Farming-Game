using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class script_EndgameController : MonoBehaviour {
	
	/*
		Simple script made by Wilson
	*/
	
	// Store each display text to display stats
	public TextMeshProUGUI days, monsters, money, score;
	
    /*
		Sets the values for each text
		function SetStats
		Input:
			days - Integer of days passed
			monsters - Integer of monsters defeated
			money - Amount of currency earned
			score - Integer of score
		Returns: None
	*/
    public void SetStats( int days, int monsters, int money, int score ) {
		
		this.days.text = days.ToString();
		this.monsters.text = monsters.ToString();
		this.money.text = money.ToString();
		this.score.text = score.ToString();
		
    }

   
}
