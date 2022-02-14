using UnityEngine;

public class Interactable : MonoBehaviour
{
    // the first bit of code here is just to draw the interaction radius in scene
    // can be changed later to work with how fine tuned we want our click accuracy to be
    // CURRENTLY ONLY WORKS IN 3D
    public float radius = 1.0f;

    // draw frame around our object
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    // if we have multiple things can be interacted with, then we call this function
    // virtual just means that we can customize it within the scripts for interactable objects
    // so like. pleacing tiles versus picking up an item - both use interaction but in different ways
    public virtual void Interact() {
        // whatever interaction is shared between methods goes here
        Debug.Log("Interaction triggered with " + transform.name);

        // then overwrite in the actual file (i.e. Items.cs) with specific interaction rules
    }

}
