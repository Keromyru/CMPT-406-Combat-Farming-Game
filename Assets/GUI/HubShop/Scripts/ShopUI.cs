using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

// Mace

// manages Shop UI displays
// goes on the actual Shop prefab
public class ShopUI : MonoBehaviour
{
    public Transform shopParent;  // the parent of all ShopSlots
    ShopSlot[] slots;  // array of Shop slots

    public GameObject shopUI;  // reference to the actual GameObject

    private Inventory inventory;  // singleton inventory

    public Transform playerInventoryParent;  // the parent of all player InventorySlots

    InventorySlot[] playerSlots;  // array of player Inventory slots

    public TMP_Text playerFunds;  // text rep of current money

    bool inRange;  // in range of hub?

    public GameObject minimap;  // minimap to disappear when shop toogles open, help with clutter

    public GameObject tracker;  // same deal as minimap, gets too cluttered

    private bool isNight = false;
    

    // Start is called before the first frame update
    void Start()
    {
        slots = shopParent.GetComponentsInChildren<ShopSlot>();
        ShopHandler.onShopRefreshCallback += UpdateUI;

        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        playerSlots = playerInventoryParent.GetComponentsInChildren<InventorySlot>();

        DayNightCycle.isNowNight += Nighttime;
        DayNightCycle.isNowDay += Daytime;
    }

    void Nighttime() {
        isNight = true;
        
        // goodbye store, we are not trapped in IKEA overnight
        shopUI.SetActive(false);
        minimap.SetActive(true);
        tracker.SetActive(true);
    }

    void Daytime() {
        isNight = false;
    }


    // checks you're near the hub
    void FixedUpdate()
    {
        if (isNight) {
            return;
        }
        
        inRange = RangeFromHub.forPlayer(0.5f);

        // yes, make shop appear, remove minimap & time tracker
        if (inRange) {
            shopUI.SetActive(true);
            minimap.SetActive(false);
            tracker.SetActive(false);
        }
        // no, hide shop and display minimap & time tracker
        else {
            shopUI.SetActive(false);
            minimap.SetActive(true);
            tracker.SetActive(true);
        }
    }


    // update UI for shop
    void UpdateUI() {

        //Debug.Log("Updating Shop UI.");

        playerFunds.text = Currency.getMoney().ToString();

        // cycle through all slots, add item if one exists in our shop list for the day
        for (int i = 0; i < slots.Length; i++) {

            ShopHandler.ShopItem current_item;

            if (i < slots.Length) {
                current_item = ShopHandler.shopItems[i];

                slots[i].AddItem(current_item.item, current_item.amount, current_item.price);

            }
            // if there isn't room, wipe the slot back to empty state
            else {
                slots[i].ClearSlot();
            }
        }

        // cycle through all slots, add item if one exists in our inventory
        for (int i = 0; i < slots.Length; i++) {

            KeyValuePair<Item, int> current_item;

            if (i < inventory.items.Count) {
                current_item = inventory.items.ElementAt(i);

                playerSlots[i].AddItem(current_item.Key, current_item.Value);

            }
            // if there isn't room, wipe the slot back to empty state
            else {
                playerSlots[i].ClearSlot();
            }
        }
    }
}
