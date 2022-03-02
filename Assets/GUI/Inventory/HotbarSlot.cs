using UnityEngine;
using UnityEngine.UI;

// Mace

public class HotbarSlot : MonoBehaviour
{
    public Image icon;  // what we want to display in inventory
    Item item;  // scriptable object that's holding our item info

    public Button background; // the background button of our slot

    // adding item to inventory
    // takes the scriptable object in as an agrument, grabs all other info from there
    public void AddItem(Item newItem) {
        item = newItem;

        icon.sprite = item.icon;  // update hotbar sprite
        icon.enabled = true;  // display hotbar sprite
    }

    // nuke all the options to nothing
    public void ClearSlot() {
        item = null;

        icon.sprite = null;

        icon.enabled = false;
    }

    // much like the UseItem() call in InventorySlot, this does nothing right now :)
    public void UseItem() {
        background.Select();
        if (item != null) {
            item.Use();
        }
    }
}
