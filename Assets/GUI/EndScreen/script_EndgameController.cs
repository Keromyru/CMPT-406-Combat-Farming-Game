using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class script_EndgameController : MonoBehaviour {
	
	// Store each display text to display stats
	public TextMeshProUGUI days, monsters, money, score;

    public void setStats() {
		this.days.text = GameStats.getTotalDays().ToString();
		this.monsters.text = GameStats.getTotalKills().ToString();
		this.money.text = GameStats.getTotalMoney().ToString();
		this.score.text = GameStats.getTotalScore().ToString();
    }
	
   
}
