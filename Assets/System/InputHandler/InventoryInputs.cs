using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

// IDEALLY - this whole script gets thrown out and this just gets integrated to the Player Controller

// this is currently just being placed on the GameManager with Inventory.cs
public class InventoryInputs : MonoBehaviour
{
    // in the actual player controller script, you would then just check if mouse down input
    // collided with an interactable via raycasting...
    // (video link: https://www.youtube.com/watch?v=9tePzyL6dgc @ 2:15)
    // and a link to the forum post I found while trying to do the mouse bit accoording to the new input system
    // https://forum.unity.com/threads/mouse-position-with-new-input-system.829248/
    
    // I've patched together a check here but it should be more streamlined for the actual build

    // Figuring out how to actually subscribe and activate an event came from here:
    // https://www.youtube.com/watch?v=zIhtPSX8hqA

    [SerializeField] private InputActionReference slot1;
    [SerializeField] private InputActionReference slot2;
    [SerializeField] private InputActionReference slot3;
    [SerializeField] private InputActionReference slot4;
    
    [SerializeField] private InputActionReference click;

    [SerializeField] private List<Item> hotbarActions;
    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;

        hotbarActions = new List<Item>();

        inventory.onItemChangedCallback += fetchHotbarItems;
    }

    void fetchHotbarItems()
    {
        int start_pos;

        hotbarActions.Clear();

        if (hotbarActions.Count < 4 && hotbarActions.Count >= 0) {
            start_pos = hotbarActions.Count;
        }
        else {
            start_pos = 4;
        }

        int stop = Mathf.Min(4, inventory.items.Count);
        
        for (int i = start_pos; i < stop; i++) {
            if (inventory.items[i] != null) {
                hotbarActions.Add(inventory.items[i]);
                Debug.Log("Added " + hotbarActions[i].name + " to hotbar actions.");
            }
        }
    }


    private void OnEnable()
    {
        slot1.action.performed += use1;
        slot1.action.Enable();

        slot2.action.performed += use2;
        slot2.action.Enable();

        slot3.action.performed += use3;
        slot3.action.Enable();

        slot4.action.performed += use4;
        slot4.action.Enable();

        click.action.performed += interactWithItem;
        click.action.Enable();
    }

    private void OnDisable()
    {
        slot1.action.performed += use1;
        slot1.action.Disable();

        slot2.action.performed += use2;
        slot2.action.Disable();

        slot3.action.performed += use3;
        slot3.action.Disable();

        slot4.action.performed += use4;
        slot4.action.Disable();

        click.action.performed -= interactWithItem;
        click.action.Disable();
    }

    private void use1(InputAction.CallbackContext context)
    {
        if (hotbarActions.Count >= 1) {
            hotbarActions[0].Use();
        }
    }

    private void use2(InputAction.CallbackContext context)
    {
        if (hotbarActions.Count >= 2) {
            hotbarActions[1].Use();
        }
    }

    private void use3(InputAction.CallbackContext context)
    {
        if (hotbarActions.Count >= 3) {
            hotbarActions[2].Use();
        }
    }

    private void use4(InputAction.CallbackContext context)
    {
        if (hotbarActions.Count == 4) {
            hotbarActions[3].Use();
        }
    }


    private void interactWithItem(InputAction.CallbackContext context)
    {
        Camera maincam = FindObjectOfType<Camera>();

        // get the mouse's current position values and cast a ray from there - 3D
        Ray ray = maincam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hitItems;

        //Debug.Log("Casting ray at: " + ray.origin);

        // on hit
        if (Physics.Raycast(ray, out hitItems, 100)) {
            Interactable interactable = hitItems.collider.GetComponent<Interactable>();
            if (interactable != null) {
                Debug.Log("Hit object " + interactable.name + " with right click");
                interactable.Interact();
            }
        }

        // get the mouse's current position values and cast a ray from there - 2D
        // shoutout to this forum post: https://answers.unity.com/questions/1087239/get-2d-collider-with-3d-ray.html
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray); 

        if (hit2D)
        {
            Interactable interactobj = hit2D.collider.GetComponent<Interactable>();
            if (interactobj != null) {
                Debug.Log("Hit 2D object " + interactobj.name);
                interactobj.Interact();
            }
        }
    }
}
