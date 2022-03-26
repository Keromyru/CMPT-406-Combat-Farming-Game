using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// Mace

// Manages the UI for the Hub Shop Slots
// need the IPointerEnterHandler & IPointerExitHandler to get mouse events on the UI
public class ShopSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;  // what we want to display in shop
    public Item item;  // scriptable object that's holding our item info

    public TMP_Text amountText;  // text to hold the amount in stack
    public int amount; // amount of item in shop

    public Button useButton; // button to buy item

    public Tooltip slotTooltipInfo;  // tooltip info for current item
    public GameObject slotTooltip;  // the actual gameobject for the tooltip

    public TMP_Text itemName;  // name of item for slot

    public TMP_Text itemPrice;  // price of the item for slot




    // adding item to inventory
    // takes the scriptable object in as an agrument, grabs all other info from there
    public void AddItem(Item newItem, int amount) {
        item = newItem;

        icon.sprite = item.icon;  // update shop sprite
        icon.enabled = true;  // display the shop sprite

        amountText.text = amount.ToString();  // update to current item stack
        amountText.enabled = true;  // display item stack

        itemName.text = item.name;

        itemPrice.text = item.price.ToString();

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

        itemName.text = "";
        itemPrice.text = "";

        useButton.interactable = false;  // no more use button

        slotTooltipInfo.ClearTooltip();  // disable tooltip for item
        slotTooltip.SetActive(false);
    }

    public void Purchased() {
        amount = amount - 1;
        amountText.text = amount.ToString();
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
