using UnityEngine;

// Mace

[CreateAssetMenu(fileName = "New Seed", menuName = "Inventory/Seed")]
public class Seed : Item
{
    // seed are always plantable
    public bool isPlantable = true;

    // you can use all the variables found in Item.cs here too
    // i.e. name, available (timeslots), price, icon, etc.

    // overrides the use method for normal items
    public override void Use() {

        // calls the Use function from Item.cs
        base.Use();

        // we can now do whatever we want

        // TODO: planting connection goes here! call planting here!
        // I'm not sure if it would be invoking a script or calling an entry function, but whatever you need to do :)
        Debug.Log("Planting seeds of type " + name);

        // I have a placeholder boolean check here so the seed only gets removed if it's successfully planted
        // right now it just auto-removes the seeds after use because there's nothing to check so...
        // it's always okay to use.
        bool wasPlanted = true;

        if (wasPlanted) {
            base.RemoveFromInventory();
        }
    }
}
