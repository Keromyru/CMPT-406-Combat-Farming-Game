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

        DayNightCycle.isNowDay += IsDay;
        DayNightCycle.isNowNight += IsNight;
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
            // Debug.Log("Inventory is full.");
            return false;
        }

        // if the item is already in inventory, just add to total
        if (items.ContainsKey(item)) {

            // if there's too many items to stack
            if (items[item] >= max_stack) {
                // Debug.Log("Cannot hold anymore items of this type.");
                return false;
            }
            
            items[item] = items[item] + 1;
        }
        // otherwise add item to inventory. we'll always start with only 1
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

        // if there's only one copy of item in inventory, nuke it
        if (items[item] == 1) {
            items.Remove(item);
        } 
        // otherwise just lower the total count
        else {
            items[item] = items[item] - 1;
        }

        SortInventory();  // sort before we send a ping to update inventory
        
        // send out an alert that inventory changed
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }


    // change to night inventory
    void IsNight() {
        day = false;
        SortInventory();
    }


    // change to day inventory
    void IsDay() {
        day = true;
        SortInventory();
    }


    // sort the inventory
    private void SortInventory() {
        // separate all the time items from night & day only items.
        Dictionary<Item, int> allDayItems = new Dictionary<Item, int>();
        Dictionary<Item, int> allNightItems = new Dictionary<Item, int>();
        Dictionary<Item, int> allUtil = new Dictionary<Item, int>();

        // sort through what we have, assign accordingly
        // available is the enum { Day, Night, Always } 
        // where Day = 0, Night = 1, and Always = 2
        foreach (KeyValuePair<Item, int> item in items) {
            if ((int)item.Key.available == 2) {
                allUtil.Add(item.Key, item.Value);
            }
            else if ((int)item.Key.available == 0) {
                allDayItems.Add(item.Key, item.Value);
            }
            else if ((int)item.Key.available == 1) {
                allNightItems.Add(item.Key, item.Value);
            }
        }

        // clear messy inventory and replace with updated order (Util -> Current Time of Day -> Unusable Items)
        items.Clear();

        // utility items always come first
        foreach (KeyValuePair<Item, int> util in allUtil) {
            items.Add(util.Key, util.Value);
        }

        // day then night items
        if (day) {
            foreach (KeyValuePair<Item, int> daytime in allDayItems) {
                items.Add(daytime.Key, daytime.Value);
            }
            foreach (KeyValuePair<Item, int> nighttime in allNightItems) {
                items.Add(nighttime.Key, nighttime.Value);
            }
        }
        // OR night then day items
        else {
            foreach (KeyValuePair<Item, int> nighttime in allNightItems) {
                items.Add(nighttime.Key, nighttime.Value);
            }
            foreach (KeyValuePair<Item, int> daytime in allDayItems) {
                items.Add(daytime.Key, daytime.Value);
            }
        }

        // send out an alert that inventory changed
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }

    public int getItemAmount(Item item){
        return items.ContainsKey(item) ? items[item] : 0;
    }
}
