using UnityEngine;
// Mace

// if you've spawned an actual GameObject for an item that the player can interact with in-game 
// (i.e. a 2D square sprite), attach this script to the actual GameObject.
public class ItemPickup : Interactable
{
    // this will be the Scriptable Object that this GameObject represents
    public Item item;

    // rules for player interaction with objects
    public override void Interact()
    {
        // callback to the Interact function from the Interactable class
        base.Interact();

        Pickup();
    }
    
    // custom pick-up actions for items, nothing too fancy right now
    void Pickup() {
        Debug.Log("Picking up " + item.name);

        // stick it into the inventory
        bool wasPickedUp = Inventory.instance.AddItem(item);
        
        if (wasPickedUp) {
            // goodbye overworld representation
            Destroy(gameObject);
        }

    }
}
