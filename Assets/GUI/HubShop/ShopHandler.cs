using System.Collections.Generic;
using UnityEngine;

// Mace

public class ShopHandler : MonoBehaviour
{
    // hacky way to basically make a dictionary that lets me use duplicate keys
    public class ShopItem {
        public Item item;
        public int amount;

        public ShopItem (Item toBuy, int total){
                item = toBuy;
                amount = total;        
        }
    }

    public static List<ShopItem> shopItems = new List<ShopItem>();

    // currency link
    private int current_money;

    //public Text displayCurrent;

    // this array is built in the Inspector!! Drag & Drop seeds in. The last element of array should always be Space Potato
    public Seed[] AllSeeds;

    // add an alert for shop refresh. to be used by the UI
    public delegate void OnShopRefresh();
    public static OnShopRefresh onShopRefreshCallback;

    int min_seeds = 3;  // minimum amount of seeds in shop stack
    int max_seeds = 15;  // maximum amount of seeds in shop stack

    private void generateItems() {

        // grab random seeds
        int pick1 = Random.Range(0, AllSeeds.Length);
        int pick2 = Random.Range(0, AllSeeds.Length);
        int pick3 = Random.Range(0, AllSeeds.Length);

        // grab random values between min & max, add to list of items
        int amount = Random.Range(min_seeds, max_seeds);
        shopItems.Add(new ShopItem(AllSeeds[pick1], amount));

        amount = Random.Range(min_seeds, max_seeds);
        shopItems.Add(new ShopItem(AllSeeds[pick2], amount));

        amount = Random.Range(min_seeds, max_seeds);
        shopItems.Add(new ShopItem(AllSeeds[pick3], amount));

        // the illusion of choice... always offer the player more space potatoes, but at a cost (you only get a few)
        shopItems.Add(new ShopItem(AllSeeds[AllSeeds.Length-1], min_seeds));
    }
    

    // Start is called before the first frame update
    void Start()
    {
        current_money = Currency.getMoney();
        //displayCurrent.text = "Current Funds: " + current_money;

        Debug.Log("Current money is " + current_money);

        for (int i = 0; i < shopItems.Count; i++) {
            Debug.Log("Item is " + shopItems[i].item + " with amount " + shopItems[i].amount);
        }

        // DayNight.StartDay += shopRefresh;

        shopRefresh();
    }

    private void shopRefresh() {
        generateItems();

        // send out an alert that inventory changed
        if (onShopRefreshCallback != null) {
            onShopRefreshCallback.Invoke();
        }
    }


    void AttemptToPurchase(Item toBuy) {
        if (current_money < toBuy.price) {
            Debug.Log("Not enough funds!");
        }
        else {
            toBuy.Buy();
        }

        current_money = Currency.getMoney();
    }
}
