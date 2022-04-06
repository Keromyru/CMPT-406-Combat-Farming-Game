using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// A static class that holds the amount of money 
public static class Currency
{
    // The amount of money in inventory (Initial amount can be changed here)
    public static int money = 500;
    // Unity Event for whenever money variable is changed
    public static UnityEvent currencyEvent = new UnityEvent();


    // Add a given amount of money to the inventory
    public static void addMoney(int moneyToAdd)
    {
        money += moneyToAdd;
        Score.addScore(moneyToAdd);
        GameStats.AddMoney(moneyToAdd);
        currencyEvent.Invoke();
    }

    // Add multiple amounts to the money counter (most likely used for selling seeds, items, etc)
    public static void addMultipleMoney(int moneyToAdd, int AmountofItemsSold)
    {
        addMoney(moneyToAdd * AmountofItemsSold);
    }

    // Subtract a given amount of money from the inventory
    public static void subtractMoney(int moneyToSubtract)
    {
        // If the player doesnt have enough money to be subtracted it gives a debug message, else it subtracts from the money variable)
        if (moneyToSubtract > money)
        {
            Debug.Log("Trying to remove too much money! Don't have enough");
        }
        else
        {
            money -= moneyToSubtract;
        }
        currencyEvent.Invoke();
    }

    // Gets the total amount of money currently in inventory (use this to get the amount instead of directly getting the money variable)
    public static int getMoney()
    {
        return money;
    }

    // Sets the total amount of money to the parameter given
    public static void setMoney(int moneyToSetTo)
    {
        money = moneyToSetTo;
        currencyEvent.Invoke();
    }
}
