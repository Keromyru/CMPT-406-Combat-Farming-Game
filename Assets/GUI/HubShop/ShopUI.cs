using UnityEngine;
using System.Collections.Generic;

// Mace

// manages Shop UI displays
public class ShopUI : MonoBehaviour
{
    public Transform shopParent;  // the parent of all ShopSlots
    ShopSlot[] slots;  // array of Shop slots

    public GameObject shopUI;  // reference to the actual GameObject

    private bool unassigned = true;  // used to see if the InputActions have been properly assigned

    private Inventory inventory;
    

    // Start is called before the first frame update
    void Start()
    {
        slots = shopParent.GetComponentsInChildren<ShopSlot>();
        ShopHandler.onShopRefreshCallback += UpdateUI;
    }

    // update UI for shop
    void UpdateUI() {

        //Debug.Log("Updating Shop UI.");

        // cycle through all slots, add item if one exists in our shop list for the day
        for (int i = 0; i < slots.Length; i++) {

            ShopHandler.ShopItem current_item;

            if (i < slots.Length) {
                current_item = ShopHandler.shopItems[i];

                slots[i].AddItem(current_item.item, current_item.amount);

            }
            // if there isn't room, wipe the slot back to empty state
            else {
                slots[i].ClearSlot();
            }
        }
    }
}
