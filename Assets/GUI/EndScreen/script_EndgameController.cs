using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class script_EndgameController : MonoBehaviour {
	
	// Store each display text to display stats
	public TextMeshProUGUI days, monsters, money, score;
	
	public Button exitBtn;
	
	
	void Start () {
		
		setStats();
		
		exitBtn.onClick.AddListener( ReturnToMenu );
		
	}
	
	public void ReturnToMenu() {
		
		GameStats.ResetAll();
		SceneManager.LoadScene( "MainMenu", LoadSceneMode.Single );
		SceneManager.LoadScene( "System", LoadSceneMode.Additive );
		
	}
		
    public void setStats() {
		this.days.text = GameStats.getTotalDays().ToString();
		this.monsters.text = GameStats.getTotalKills().ToString();
		this.money.text = GameStats.getTotalMoney().ToString();
		this.score.text = GameStats.getTotalScore().ToString();
    }
	
   
}
