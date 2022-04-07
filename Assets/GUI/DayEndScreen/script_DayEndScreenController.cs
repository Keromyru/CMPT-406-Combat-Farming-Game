using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class script_DayEndScreenController : MonoBehaviour {
	
	// Store each display text to display stats
	public TextMeshProUGUI days, monsters, money, score;

	public Animator animator;
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
	
	void Start(){
		DayNightCycle.isNowDay += setDataOnNewDay;
	}

	public void setDataOnNewDay(){
		this.setMonsters(GameStats.getNightKills());
		this.setMoney(GameStats.getNightMoney());
		this.setScore(GameStats.getNightScore());
		GameStats.NightStatReset();
		StartCoroutine(animationControl());
	}

	IEnumerator animationControl(){
		animator.Play("anim_DayEndScreenEnter");
		yield return new WaitForSeconds(4f);
		animator.Play("anim_DayEndScreenExit");
	}

    public void SetStats( int days, int monsters, int money, int score ) {
		
		this.days.text = days.ToString();
		this.monsters.text = monsters.ToString();
		this.money.text = money.ToString();
		this.score.text = score.ToString();
		
    }

	public void setDays(int days){
		this.days.text = days.ToString();
	}

	public void setMonsters(int monsters){
		this.monsters.text = monsters.ToString();
	}

	public void setMoney(int money){
		this.money.text = money.ToString();
	}
	
	public void setScore(int score){
		this.score.text = score.ToString();
	}
}
