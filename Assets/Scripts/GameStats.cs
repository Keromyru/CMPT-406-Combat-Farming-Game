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
    }

    public static void AddMoney(int newMoney)
    {
        nightMoney += newMoney;
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
    #endregion Sets and Gets

}
