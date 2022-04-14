using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// A static class that holds some of the stats
// Blake

public static class GameStats
{
    static int nightScore = 0;
    static int nightMoney = 0;
    static int nightKills = 0;
    static int totalKills = 0;

    static int totalMoney = 0;

    static int totalScore = 0;

    static int totalDays = 0;
	
	public static void ResetAll() {
		
		nightScore = 0;
		nightMoney = 0;
		nightKills = 0;
		totalKills = 0;
		totalMoney = 0;
		totalScore = 0;
		totalDays = 0;
	
	}

    public static void NightStatReset()
    {
        nightMoney = 0;
        nightKills = 0;
        nightScore = 0;
    }

    public static void AddKill()
    {
        nightKills ++;
        totalKills ++;
    }

    public static void AddScore(int newScore)
    {
        nightScore += newScore;
        totalScore += newScore;
    }

    public static void AddMoney(int newMoney)
    {
        nightMoney += newMoney;
        totalMoney += newMoney;
    }

    public static void AddDay(int day){
        totalDays += day;
    }

    #region Sets and Gets
    public static int getNightMoney()
    {
        return nightMoney;
    }

    public static int getNightKills()
    {
        return nightKills;
    }

    public static int getNightScore()
    {
        return nightScore;
    }

    public static int getTotalKills(){
        return totalKills;
    }

    public static int getTotalScore(){
        return totalScore;
    }

    public static int getTotalMoney(){
        return totalMoney;
    }

    public static int getTotalDays(){
        return totalDays;
    }
    #endregion Sets and Gets

}
