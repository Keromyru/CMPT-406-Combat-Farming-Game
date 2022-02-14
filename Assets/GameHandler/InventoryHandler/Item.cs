using UnityEngine;

// makes a new tab appear in the actual right click menu in Unity specifically for creating new Inventory Items
// selecting it just generates an empty item that you can customize :)
// if we want this option out of the menu, you can just comment the following line
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]

public class Item : ScriptableObject
{

    // only need the *new* in front because this overrides Unity's default name variable
    new public string name = "New Item";

    // visual rep of item for inventory display
    public Sprite icon = null;

    // is this an item that can be sold?
    public bool isSellable = false;

    // price point for selling
    public int pricePoint = 0;

    // is this a seed or plantable object?
    public bool isPlantable = false;

    // if we're doing the split combat versus farming inventory
    // I would like to add an int check here, something like
    // public int available = (0 = all the time, 1 = day time only, 2 = night time only)

}
