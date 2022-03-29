using System.Collections.Generic;
using UnityEngine;

// Mace

// Attached to the ShopHandler object, in GameHandler
public class ShopHandler : MonoBehaviour
{
    // hacky way to basically make a dictionary that lets me use duplicate keys
    public class ShopItem {
        public Item item;
        public int amount;
        public int price;

        public ShopItem (Item toBuy, int total, int value){
                item = toBuy;
                amount = total;
                price = value;    
        }
    }

    public static List<ShopItem> shopItems = new List<ShopItem>();

    // currency link
    private int current_money;

    // this array is built in the Inspector!! Drag & Drop seeds in. The last element of array should always be Space Potato
    // this is saved in the prefab for the ShopHandler so you should be able to drag-and-drop :)
    public Seed[] AllSeeds;

    // add an alert for shop refresh. to be used by the UI
    public delegate void OnShopRefresh();
    public static OnShopRefresh onShopRefreshCallback;

    int min_seeds = 3;  // minimum amount of seeds in shop stack
    int max_seeds = 15;  // maximum amount of seeds in shop stack

    public float markup = 1.3f;  // mark items up to 30% in shop

    public float special_discount = 1.15f;  // mark speciality items up to 15% in shop


    // generate a list of items for sale
    private void generateItems() {

        shopItems.Clear();  // kill the old shop. new shop now.

        // grab random seeds
        int pick1 = Random.Range(0, AllSeeds.Length);
        int pick2 = Random.Range(0, AllSeeds.Length);
        int pick3 = Random.Range(0, AllSeeds.Length);

        // grab random values between min & max, add to list of items
        int amount = Random.Range(min_seeds, max_seeds);
        shopItems.Add(new ShopItem(AllSeeds[pick1], amount, (int)(Mathf.Round(AllSeeds[pick1].price * special_discount))));

        amount = Random.Range(min_seeds, max_seeds);
        shopItems.Add(new ShopItem(AllSeeds[pick2], amount, (int)Mathf.Round(AllSeeds[pick2].price * markup)));

        amount = Random.Range(min_seeds, max_seeds);
        shopItems.Add(new ShopItem(AllSeeds[pick3], amount, (int)Mathf.Round(AllSeeds[pick3].price * markup)));

        // the illusion of choice... always offer the player more space potatoes, but at a cost (you only get a few)
        shopItems.Add(new ShopItem(AllSeeds[AllSeeds.Length-1], min_seeds, (int)Mathf.Round(AllSeeds[AllSeeds.Length-1].price * markup)));
    }
    

    // get money, get items, set the refresh call
    void Awake()
    {
        current_money = Currency.getMoney();
        generateItems();  
        DayNightCycle.isNowDay += shopRefresh;
    }


    // generate a new list of items
    private void shopRefresh() {

        //Debug.Log("SHOP REFRESHED?");
        generateItems();

        // send out an alert that shop changed
        if (onShopRefreshCallback != null) {
            onShopRefreshCallback.Invoke();
        }
    }


    // try to sell the item - will also remove it from inventory
    public void AttemptToSell(InventorySlot toSell) {

        if (toSell.amount < 0) {
            Debug.Log("Trying to sell something that isn't in stock.");
            return;
        }

        Item item = toSell.item;

        item.Sell();
        Inventory.instance.RemoveItem(item);

        current_money = Currency.getMoney();

        // send out an alert that shop changed
        if (onShopRefreshCallback != null) {
            onShopRefreshCallback.Invoke();
        }

    }


    // try to buy something from shop and add it to inventory
    public void AttemptToPurchase(ShopSlot toBuy) {

        // extremely hacky way to get the current slot
        // grab the name of the slot from scene, shorten it only the char holding slot number
        // convert back to string then grab an int out of the string
        // is this over complicated? absolutely! does it work? yes! :)
        int current = int.Parse(toBuy.name[0].ToString());

        ShopItem shop_item = shopItems[current];

        // is it in stock?
        if (shop_item.amount <= 0) {
            Debug.Log("Trying to buy something that isn't in stock.");
            return;
        }

        Item item = shop_item.item;

        // can we afford it?
        // no
        if (current_money < shop_item.price) {
            Debug.Log("Not enough funds!");
        }
        // yes
        else {
            item.Buy(shop_item.price);
            Inventory.instance.AddItem(item);
            shopItems[current].amount -= 1;
        }

        current_money = Currency.getMoney();

        // send out an alert that shop changed
        if (onShopRefreshCallback != null) {
            onShopRefreshCallback.Invoke();
        }
    }
}
