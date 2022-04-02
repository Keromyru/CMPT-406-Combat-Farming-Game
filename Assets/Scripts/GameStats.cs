using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A static class that holds some of the stats
public static class NightStats
{
    public static int nightMoney = 0;
    public static int totalMoney = 0;
    public static int nightKills = 0;
    public static int totalKills = 0;
    public static int nightScore = 0;
    public static int totalScore = 0;

    public static int getNightMoney()
    {
        return nightMoney;
    }

    public static void setNightMoney(int money)
    {
        nightMoney = money;
    }

    public static int getNightKills()
    {
        return nightKills;
    }

    public static void setNightKills(int kills)
    {
        nightKills = kills;
    }

    public static int getNightScore()
    {
        return nightScore;
    }

    public static void setNightScore(int score)
    {
        nightScore = score;
    }

    public static void NightStatReset()
    {
        nightMoney = 0;
        nightKills = 0;
        nightScore = 0;
    }
}
