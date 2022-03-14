using UnityEngine;
using UnityEngine.UI;

// Mace

// very simple script to make Item tooltips
// needs to be attached to the Tooltip GameObject (found on InventorySlot)
public class Tooltip : MonoBehaviour
{
    // info to display in the tooltip
    public Text itemName;
    public Text itemDescription;
    public Text itemStats;

    // update the tooltip description upon adding an item
    // just grabs the information from the Item SO.
    public void UpdateTooltip(Item item) {
        itemName.text = item.name;

        itemDescription.text = item.description;

        itemStats.text = BuildStats(item);
    }


    // no item? no info.
    public void ClearTooltip() {
        itemName.text = "";
        itemDescription.text = "";
        itemStats.text = "";
    }


    // build the information for the stats box from the rest of the options for
    // the Item SOs.
    private string BuildStats(Item item) {
        // string for each stat to keep it easy to follow
        string sellInfo;
        string whenToUse = "Item can ";


        // build sell information
        if (item.isSellable) {
            sellInfo = "Sell Price: " + item.price;
        }
        else {
            sellInfo = "Not For Sale.";
        }

        // build availability information
        if ((int)item.available == 0) {
            whenToUse += "only be used during the day!";
        }
        else if ((int)item.available == 1) {
            whenToUse += "only be used during the night!";
        }
        else {
            whenToUse += "be used all the time!";
        }

        // make one string & send it back for display
        string finalStats = whenToUse + "\n" + sellInfo;

        return finalStats;
    }
}
