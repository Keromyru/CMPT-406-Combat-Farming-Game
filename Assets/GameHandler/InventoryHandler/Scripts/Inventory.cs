using System.Collections.Generic;
using UnityEngine;

// Mace

// Attached to the InventoryHandler (GameHandler->ObjectHandler->InventoryHandler)
public class Inventory : MonoBehaviour
{
    // add an alert for changes in inventory. to be used by the UI for hotbar and inventory
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    
    
    // adding a singleton structure so we can't have two inventories floating around
    // any interactions with the Inventory system (i.e. adding objects) can then call
    // Inventory.instance.method() instead of having to locate the Inventory every time
    public static Inventory instance;

    void Awake()
    {
        if (instance != null) {
            Debug.LogWarning("Trying to create more than one inventory system!");
            return;
        }
        instance = this;
    }
    // end adding singleton
    

    // this is the main inventory - literally just a built-in list that uses... list operations for management.
    public Dictionary<Item, int> items = new Dictionary<Item, int>();

    // max inventory spots
    // if this is changed, you'll have to physically go into the scene & add inventory slots to match
    public int max_space = 16;

    // same deal here for the hotbar
    public int max_hotbar_space = 8;

    // is it day or night time?
    private bool day = true;

    // what is the maximum number of items that can stack?
    public int max_stack = 99;


    // just a list update
    // boolean return - true on success, false on failure
    public bool AddItem(Item item) {
        // is there space for an item?
        if (items.Count >= max_space) {
            Debug.Log("Inventory is full.");
            return false;
        }

        if (items.ContainsKey(item)) {
            // if there's too many items to stack
            if (items[item] >= 99) {
                Debug.Log("Cannot hold anymore items of this type.");
                return false;
            }
            
            items[item] = items[item] + 1;
        }
        else {
            items.Add(item, 1);
        }

        SortInventory();  // sort before we send a ping to update inventory
        
        // send out an alert that inventory changed
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
        
        return true;
    }

    // just another list update
    public void RemoveItem(Item item) {

        if (items[item] == 1) {
            items.Remove(item);
        } 
        else {
            items[item] = items[item] - 1;
        }

        SortInventory();  // sort before we send a ping to update inventory
        
        // send out an alert that inventory changed
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }


    // sort the inventory
    private void SortInventory() {
        // separate all the time items from night & day only items.
        Dictionary<Item, int> allDayItems = new Dictionary<Item, int>();
        Dictionary<Item, int> allNightItems = new Dictionary<Item, int>();
        Dictionary<Item, int> allUtil = new Dictionary<Item, int>();

        // sort through what we have, assign accordingly
        foreach (KeyValuePair<Item, int> item in items) {
            if (item.Key.availableDay && item.Key.availableNight) {
                allUtil.Add(item.Key, item.Value);
            }
            else if (item.Key.availableDay) {
                allDayItems.Add(item.Key, item.Value);
            }
            else if (item.Key.availableNight) {
                allNightItems.Add(item.Key, item.Value);
            }
        }

        // clear messy inventory and replace with updated order (Util -> Current Time of Day -> Unusable Items)
        items.Clear();

        foreach (KeyValuePair<Item, int> util in allUtil) {
            items.Add(util.Key, util.Value);
        }

        if (day) {
            foreach (KeyValuePair<Item, int> daytime in allDayItems) {
                items.Add(daytime.Key, daytime.Value);
            }

            foreach (KeyValuePair<Item, int> nighttime in allNightItems) {
                items.Add(nighttime.Key, nighttime.Value);
            }
        }
        else {
            foreach (KeyValuePair<Item, int> nighttime in allNightItems) {
                items.Add(nighttime.Key, nighttime.Value);
            }
            
            foreach (KeyValuePair<Item, int> daytime in allDayItems) {
                items.Add(daytime.Key, daytime.Value);
            }
        }
    }
}
