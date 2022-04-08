using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyDemands : MonoBehaviour
{
    // To allow dialogues for the demands
    private Dialogue dialogue;
    public DialogueHandler dialogueHandler;

    // All the demands info
    private int[] prices = new int[3] { 100, 500, 1000 };
    private int priceIndex = 0;
    private int companyTaxDay = 5;

    // Keeps count of days
    public int dayCount = -1;

    // Start is called before the first frame update
    void Start()
    {
        DayNightCycle.isNowDay += CheckDay;
        dialogue = new Dialogue();
    }

    // At start of day checks if it is a tax day. If it is then player gets money taken (if they dont have enough then lose condition).
    private void CheckDay()
    {
        dayCount += 1;

        if (dayCount <= (prices.Length * companyTaxDay))
        {
            if (dayCount % companyTaxDay == 0 && dayCount != 0)
            {
                // Lose condition goes here
                if (Currency.getMoney() < prices[priceIndex])
                {
                    Debug.Log("You Lose game from Company Demands");
                    //TODO
                }
                // Winning condition goes here
                else
                {
                    Currency.subtractMoney(prices[priceIndex]);

                    // Dialogue box to show next company demand
                    priceIndex += 1;
                    dialogue.name = "EXO Corp";

                    if (priceIndex < prices.Length)
                    {
                        dialogue.sentences = new string[1] { "Thanks for the " + prices[priceIndex] + " dollars. If there isn't " + prices[priceIndex] + " in your bank in " + companyTaxDay + " days, be ready to lose your job." };
                        dialogueHandler.StartDialogue(dialogue);
                    }
                    else
                    {
                        dialogue.sentences = new string[1] { "Thanks for the " + prices[priceIndex - 1] + " dollars. Keep working hard!" };
                        dialogueHandler.StartDialogue(dialogue);
                    }
                    
                    // Some type of reward?
                }
            }
        }
        else
        {
            DayNightCycle.isNowDay -= CheckDay;
        }
    }
}
