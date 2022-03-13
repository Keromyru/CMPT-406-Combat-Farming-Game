using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Currency
{
    public static int money = 500;

    public static void addMoney(int moneyToAdd)
    {
        money += moneyToAdd;
    }

    public static void subtractMoney(int moneyToSubtract)
    {
        if (moneyToSubtract > money)
        {
            Debug.Log("Trying to remove too much money! Don't have enough");
        }
        else
        {
            money -= moneyToSubtract;
        }
    }

    public static int getMoney()
    {
        return money;
    }
}
