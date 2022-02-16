using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to the InventoryHandler (GameHandler->ObjectHandler->InventoryHandler)
public class Inventory : MonoBehaviour
{
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

    // just a list update
    public void AddItem(Item item) {
        items.Add(item);
    }

    // just another list update
    public void RemoveItem(Item item) {
        items.Remove(item);
    }
}
