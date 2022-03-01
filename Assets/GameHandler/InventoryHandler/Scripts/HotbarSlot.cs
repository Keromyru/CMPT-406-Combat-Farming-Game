using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{
    public Image icon;
    Item item;

    public void AddItem(Item newItem) {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot() {
        item = null;

        icon.sprite = null;

        icon.enabled = false;
    }

    // not really needed? need some way to split them up by key
    public void UseItem() {
        if (item != null) {
            item.Use();
        }
    }
}
