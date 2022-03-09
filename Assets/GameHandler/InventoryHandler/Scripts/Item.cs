using UnityEngine;
// Mace

// makes a new tab appear in the actual right click menu in Unity specifically for creating new Inventory Items
// selecting it just generates an empty item that you can customize :)
// if we want this option out of the menu, you can just comment the following line
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
// I've been putting created items in Entities -> Items -> SubFolder
// i.e. all the Plant Seeds are Entities -> Items -> Seeds

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

    // for split inventory, what timeslots is the item available for?
    public bool availableNight = false;
    public bool availableDay = true;

    // default use function that can be shared amongst all Items (including Item sub-types)
    // works just like Interact() in Interactable.cs does, just be sure to include
    // "public virtual void Use() { Base.Use(); AND THEN ANY CODE YOU WANT; }"
    public virtual void Use() {
        Debug.Log("Using " + name);
    }

}