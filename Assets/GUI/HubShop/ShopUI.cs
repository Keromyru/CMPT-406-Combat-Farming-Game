using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

// Mace

// manages Shop UI displays
public class ShopUI : MonoBehaviour
{
    public Transform shopParent;  // the parent of all ShopSlots
    ShopSlot[] slots;  // array of Shop slots

    public GameObject shopUI;  // reference to the actual GameObject

    private Inventory inventory;

    public Transform playerParent;  // the parent of all player InventorySlots

    InventorySlot[] playerSlots;  // array of player Inventory slots

    public TMP_Text playerFunds;
    

    // Start is called before the first frame update
    void Start()
    {
        slots = shopParent.GetComponentsInChildren<ShopSlot>();
        ShopHandler.onShopRefreshCallback += UpdateUI;

        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        playerSlots = playerParent.GetComponentsInChildren<InventorySlot>();
    }

    // update UI for shop
    void UpdateUI() {

        Debug.Log("Updating Shop UI.");

        playerFunds.text = Currency.getMoney().ToString();

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
