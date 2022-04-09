using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyDemands : MonoBehaviour
{
    // To allow dialogues for the demands
    private Dialogue dialogue;
    public DialogueHandler dialogueHandler;
    public GameObject player;
    public PlayerOnAttackSO gunUpgrade;
    private PlayerController playerController;

    // All the demands info
    public int[] prices;
    private int priceIndex = 0;
    public int companyTaxDay;

    // Keeps count of days
    private int dayCount = -1;

    // Start is called before the first frame update
    void Start()
    {
        DayNightCycle.isNowDay += CheckDay;
        dialogue = new Dialogue();
        playerController = player.GetComponent<PlayerController>();
    }

    // At start of day checks if it is a tax day. If it is then player gets money taken (if they dont have enough then lose condition).
    private void CheckDay()
    {
        dayCount += 1;

        // For first message
        if (dayCount == 0)
        {
            dialogue.name = "EXO Corp";
            dialogue.sentences = new string[2] { "This is your boss speaking. If you need any help with controls then click the tutorial button later.", "You owe money to the company. If there isn't " + prices[0] +
                " coins in your bank in " + companyTaxDay + " days then be ready to lose more than just your job." };
            dialogueHandler.StartDialogue(dialogue);
        }

        // For every CompanyTaxDay
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
                        dialogue.sentences = new string[1] { "Thanks for the " + prices[priceIndex - 1] + " dollars. But if there isn't " + prices[priceIndex] + " in your " +
                            "bank in " + companyTaxDay + " days then be ready to lose more than just your job." };
                    }
                    else
                    {
                        dialogue.sentences = new string[1] { "Thanks for the " + prices[priceIndex - 1] + " dollars. We have decided to stop taxing you for now, keep working hard!" };
                    }

                    // Day 10 weapon upgrade
                    if (dayCount == 10)
                    { 
                        dialogue.sentences = new string[2] { dialogue.sentences[0], "For surviving this long we at the Corporation decided to give you an reward. We have upgraded your gun, have fun with it." };

                        playerController.setNewOnAttack(gunUpgrade);
                    }
                    
                    dialogueHandler.StartDialogue(dialogue);
                }
            }
        }
        // Company demands are done
        else
        {
            DayNightCycle.isNowDay -= CheckDay;
        }
    }
}
