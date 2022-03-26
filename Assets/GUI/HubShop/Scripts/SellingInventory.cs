using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// Mace

// manages Inventory and Hotbar UI displays
public class SellingInventory : MonoBehaviour
{
    Inventory inventory;  // get singleton inventory
    public Transform itemsParent;  // the parent of all InventorySlots
    InventorySlot[] slots;  // array of Inventory slots

    // Start is called before the first frame update
    void Start()
    {
        // grab singleton & subscribe to changes in items
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        // fill arrays with all the slots on scene
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // update UI for hotbar & inventory
    void UpdateUI() {
        // cycle through all slots, add item if one exists in our inventory
        for (int i = 0; i < slots.Length; i++) {

            KeyValuePair<Item, int> current_item;

            if (i < inventory.items.Count) {
                current_item = inventory.items.ElementAt(i);

                slots[i].AddItem(current_item.Key, current_item.Value);

            }
            // if there isn't room, wipe the slot back to empty state
            else {
                slots[i].ClearSlot();
            }
        }
    }
}
