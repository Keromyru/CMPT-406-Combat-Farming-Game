using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// A static class that holds the amount of score 
public static class Score
{
    // The amount of score (Initial amount can be changed here)
    public static int score = 500;
    // Unity Event for whenever score variable is changed
    public static UnityEvent scoreEvent = new UnityEvent();


    // Add a given amount of score
    public static void addScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreEvent.Invoke();
    }

    // Add multiple amounts to the score counter
    public static void addMultipleScore(int scoreToAdd, int AmountofItemsSold)
    {
        score += (scoreToAdd * AmountofItemsSold);
        scoreEvent.Invoke();
    }

    // Subtract a given amount of score from the inventory
    public static void subtractScore(int scoreToSubtract)
    {
        // If the player doesnt have enough score to be subtracted it gives a debug message, else it subtracts from the score variable)
        if (scoreToSubtract > score)
        {
            Debug.Log("Trying to remove too much score! Don't have enough");
        }
        else
        {
            score -= scoreToSubtract;
        }
        scoreEvent.Invoke();
    }

    // Gets the total amount of score (use this to get the amount instead of directly getting the score variable)
    public static int getScore()
    {
        return score;
    }

    // Sets the total amount of score to the parameter given
    public static void setScore(int scoreToSetTo)
    {
        score = scoreToSetTo;
        scoreEvent.Invoke();
    }
}
