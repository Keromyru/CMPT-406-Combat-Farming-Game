using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    

    // this is the main inventory - literally just a built in list that uses... list operations for management.
    public List<Item> items = new List<Item>();

    // max inventory spots
    public int max_space = 16;

    // just a list update
    public bool AddItem(Item item) {
        // is there space for an item?
        if (items.Count >= max_space) {
            Debug.Log("Inventory is full.");
            return false;
        }
        items.Add(item);
        
        // send out an alert that inventory changed
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
        
        return true;
    }

    // just another list update
    public void RemoveItem(Item item) {
        items.Remove(item);
        
        // send out an alert that inventory changed
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }
}
