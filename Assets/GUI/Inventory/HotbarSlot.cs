using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Mace

// Manages the UI for the Hotbar Slots
// need the IPointerEnterHandler & IPointerExitHandler to get mouse events on the UI
public class HotbarSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;  // what we want to display in inventory
    Item item;  // scriptable object that's holding our item info

    public Toggle background; // the background toggle of our slot

    public Text amountText;  // text to hold the amount in stack
    public int amount; // amount of item in inventory

    public bool canBeUsed = false;  // can the slot itself be used right now?

    public Tooltip slotTooltipInfo;  // tooltip info for current item
    public GameObject slotTooltip;  // the actual gameobject for the tooltip


    // adding item to inventory
    // takes the scriptable object in as an agrument, grabs all other info from there
    public void AddItem(Item newItem, int amount) {
        item = newItem;

        icon.sprite = item.icon;  // update hotbar sprite
        icon.enabled = true;  // display hotbar sprite

        amountText.text = amount.ToString();  // update to current item stack
        amountText.enabled = true;  // display item stack

        slotTooltipInfo.UpdateTooltip(newItem);  // update tooltip information
    }

    // nuke all the options to nothing
    public void ClearSlot() {
        item = null;

        icon.sprite = null;

        icon.enabled = false;

        amountText.text = "0";
        amountText.enabled = false;

        slotTooltipInfo.ClearTooltip();  // disable tooltip for item
        slotTooltip.SetActive(false);
    }

    // much like the UseItem() call in InventorySlot, this does nothing right now :)
    public void UseItem() {
        if (!canBeUsed) {
            Debug.Log("Cannot use item right now.");
            return;
        }

        if (item != null) {
            item.Use();
            background.isOn = true;
        }
    }

    public void DeselectItem() {
        background.isOn = false;
    }

    public void SelectItem() {
        background.isOn = true;
    }

    // if we can't use the slot at present (i.e. wrong time of day)
    public void MarkUnusable() {
        // dim the sprite
        Color faded = icon.color;
        faded.a = 0.5f;
        icon.color = faded;

        canBeUsed = false;

        //Debug.Log("Marking current hotbar slot unusable");
    }

    // if we can use this slot
    public void MarkUsable() {
        // brighten the sprite
        Color full = icon.color;
        full.a = 1.0f;
        icon.color = full;

        canBeUsed = true;

        //Debug.Log("Can now use this hotbar slot.");
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
}
