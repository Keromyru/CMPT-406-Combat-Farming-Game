using UnityEngine;
using UnityEngine.UI;

// Mace

// Manages the UI for the Inventory Slots
public class InventorySlot : MonoBehaviour
{
    public Image icon;  // what we want to display in inventory
    Item item;  // scriptable object that's holding our item info

    public Button removeButton;  // literally a button on the slot

    public Text amountText;  // text to hold the amount in stack
    public int amount; // amount of item in inventory

    // adding item to inventory
    // takes the scriptable object in as an agrument, grabs all other info from there
    public void AddItem(Item newItem, int amount) {
        item = newItem;

        icon.sprite = item.icon;  // update inventory sprite
        icon.enabled = true;  // display the inventory sprite

        amountText.enabled = true;
        amountText.text = amount.ToString();

        removeButton.interactable = true;  // we can now click the delete button
    }

    // nuke all the options to nothing
    public void ClearSlot() {
        item = null;

        icon.sprite = null;

        icon.enabled = false;

        amountText.text = "0";
        amountText.enabled = false;

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

    // if we can't use the slot at present (i.e. wrong time of day)
    public void MarkUnusable() {
        Debug.Log("Marking current slot unusable");
    }

    // if we can use this slot
    public void MarkUsable() {
        Debug.Log("Can now use this slot.");
    }
}
