using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Mace

// Manages the UI for the Inventory Slots
// need the IPointerEnterHandler & IPointerExitHandler to get mouse events on the UI
public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;  // what we want to display in inventory
    public Item item;  // scriptable object that's holding our item info

    public Button removeButton;  // literally a button on the slot

    public Text amountText;  // text to hold the amount in stack
    public int amount; // amount of item in inventory

    public Button useButton; // button to use item

    public Tooltip slotTooltipInfo;  // tooltip info for current item
    public GameObject slotTooltip;  // the actual gameobject for the tooltip


    // adding item to inventory
    // takes the scriptable object in as an agrument, grabs all other info from there
    public void AddItem(Item newItem, int amount) {
        item = newItem;

        icon.sprite = item.icon;  // update inventory sprite
        icon.enabled = true;  // display the inventory sprite

        amountText.text = amount.ToString();  // update to current item stack
        amountText.enabled = true;  // display item stack

        removeButton.interactable = true;  // we can now click the delete button
        useButton.interactable = true;  // can now use the item as well

        slotTooltipInfo.UpdateTooltip(newItem);  // update tooltip information
    }

    // nuke all the options to nothing
    public void ClearSlot() {
        item = null;

        icon.sprite = null;

        icon.enabled = false;

        amountText.text = "0";
        amountText.enabled = false;

        removeButton.interactable = false;  // no more remove button
        useButton.interactable = false;  // no more use button

        slotTooltipInfo.ClearTooltip();  // disable tooltip for item
        slotTooltip.SetActive(false);
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

    // sells the item for its sell amount
    public void SellItem() {
        if (item != null) {
            item.Sell();
        }
    }

    // if we can't use the slot at present (i.e. wrong time of day)
    public void MarkUnusable() {
        // dim the sprite
        Color faded = icon.color;
        faded.a = 0.5f;
        icon.color = faded;

        // remove interactions
        removeButton.interactable = false;
        useButton.interactable = false;

        //Debug.Log("Marking current slot unusable");
    }

    // if we can use this slot
    public void MarkUsable() {
        // brighten the sprite
        Color full = icon.color;
        full.a = 1.0f;
        icon.color = full;

        // make clickeys clickable again
        removeButton.interactable = true;
        useButton.interactable = true;

        //Debug.Log("Can now use this slot.");
    }

    // this is the new version of OnMouseEnter(), or hovering the item slot
    public void OnPointerEnter(PointerEventData eventData)
    {
        // only pop the tooltip up if there's actually info to display
        if (icon.sprite != null) {
            slotTooltip.SetActive(true);
        }
    }

    // this is the new version of OnMouseExit(), or stop hovering item slot
    public void OnPointerExit(PointerEventData eventData)
    {
        // no more tooltip
        slotTooltip.SetActive(false);
    }

    // for the first eight slots, selects the corresponding section on the hotbar too
    public void SelectSlot() {
    
        string pressed_button = this.gameObject.name;
        
        // Debug.Log("pressed " + pressed_button);

        int picked_item = int.Parse(pressed_button[15].ToString()) - 1;

        InventoryInputs.instance.InventoryClick(picked_item);
    }
}
