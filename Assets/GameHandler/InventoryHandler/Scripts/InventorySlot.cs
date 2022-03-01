using UnityEngine;
using UnityEngine.UI;

// Mace

// Manages the UI for the Inventory Slots
public class InventorySlot : MonoBehaviour
{
    public Image icon;  // what we want to display in inventory
    Item item;  // scriptable object that's holding our item info

    public Button removeButton;  // literally a button on the slot

    // adding item to inventory
    // takes the scriptable object in as an agrument, grabs all other info from there
    public void AddItem(Item newItem) {
        item = newItem;

        icon.sprite = item.icon;  // update inventory sprite
        icon.enabled = true;  // display the inventory sprite

        removeButton.interactable = true;  // we can now click the delete button
    }

    // nuke all the options to nothing
    public void ClearSlot() {
        item = null;

        icon.sprite = null;

        icon.enabled = false;

        removeButton.interactable = false;  // no more remove button
    }

    // when remove button is clicked, delete the item it's attached to
    public void OnRemoveButton() {
        Inventory.instance.RemoveItem(item);
    }

    // using item. calls use. does. absolutely nothing right now.
    public void UseItem() {
        if (item != null) {
            item.Use();
        }
    }
}
