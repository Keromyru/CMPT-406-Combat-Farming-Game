using UnityEngine;

public class ItemPickup : Interactable
{
    // it's the item to be picked up
    public Item item;

    public override void Interact()
    {
        // callback to the Interact function from the Interactable class
        base.Interact();

        // custom pick-up actions for items
        Pickup();
    }

    void Pickup() {
        Debug.Log("Picking up " + item.name);

        // stick it into the inventory
        Inventory.instance.AddItem(item);

        // goodbye overworld representation
        Destroy(gameObject);
    }
}
