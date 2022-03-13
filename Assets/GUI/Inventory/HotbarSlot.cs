using UnityEngine;
using UnityEngine.UI;

// Mace

public class HotbarSlot : MonoBehaviour
{
    public Image icon;  // what we want to display in inventory
    Item item;  // scriptable object that's holding our item info

    public Toggle background; // the background toggle of our slot

    public Text amountText;  // text to hold the amount in stack
    public int amount; // amount of item in inventory

    // adding item to inventory
    // takes the scriptable object in as an agrument, grabs all other info from there
    public void AddItem(Item newItem, int amount) {
        item = newItem;

        icon.sprite = item.icon;  // update hotbar sprite
        icon.enabled = true;  // display hotbar sprite

        amountText.text = amount.ToString();  // update to current item stack
        amountText.enabled = true;  // display item stack
    }

    // nuke all the options to nothing
    public void ClearSlot() {
        item = null;

        icon.sprite = null;

        icon.enabled = false;

        amountText.text = "0";
        amountText.enabled = false;
    }

    // much like the UseItem() call in InventorySlot, this does nothing right now :)
    public void UseItem() {
        background.isOn = true;
        if (item != null) {
            item.Use();
        }
    }

    // if we can't use the slot at present (i.e. wrong time of day)
    public void MarkUnusable() {
        // dim the sprite
        Color faded = icon.color;
        faded.a = 0.5f;
        icon.color = faded;

        //Debug.Log("Marking current slot unusable");
    }

    // if we can use this slot
    public void MarkUsable() {
        // brighten the sprite
        Color full = icon.color;
        full.a = 1.0f;
        icon.color = full;

        //Debug.Log("Can now use this slot.");
    }
}
