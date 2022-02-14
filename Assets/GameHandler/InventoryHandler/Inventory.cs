using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // adding a singleton structure so we can't have two inventories floating around
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
    
    // this is the main inventory
    public List<Item> items = new List<Item>();

    public void AddItem(Item item) {
        items.Add(item);
    }

    public void RemoveItem(Item item) {
        items.Remove(item);
    }
}
