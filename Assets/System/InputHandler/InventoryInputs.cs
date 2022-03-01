using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
// Mace

// IDEALLY - this whole script gets reworked to work better with the InputActions, but it is functional as is

// this is currently attached to the InventoryHandler (prefab)
public class InventoryInputs : MonoBehaviour
{
    // had issues with using InputActionMaps and just grabbed everything individually
    [SerializeField] private InputActionReference slot1;
    [SerializeField] private InputActionReference slot2;
    [SerializeField] private InputActionReference slot3;
    [SerializeField] private InputActionReference slot4;
    
    // this is currently player primary (i.e. left click)
    [SerializeField] private InputActionReference click;

    // tracking what items are in the health bar
    [SerializeField] private List<Item> hotbarActions;
    // singleton inventory
    Inventory inventory;

    void Start()
    {
        // set inventory to existing instance, make list for hotbar actions, subscribe to item changes
        inventory = Inventory.instance;
        hotbarActions = new List<Item>();
        inventory.onItemChangedCallback += fetchHotbarItems;
    }

    // build list of hotbar items
    // wrote this on very little sleep. could be improved, but once again, it's functional
    void fetchHotbarItems()
    {
        int start_pos;  // position to start building at

        hotbarActions.Clear();  // destroy the old list. we're updating.

        if (hotbarActions.Count < 4 && hotbarActions.Count >= 0) {
            start_pos = hotbarActions.Count;  // in the middle of the hotbar
        }
        else {
            start_pos = 4;  // hotbar is filled
        }

        int stop = Mathf.Min(4, inventory.items.Count);  // stop at max health bar or max items in inventory
        
        // add item
        for (int i = start_pos; i < stop; i++) {
            if (inventory.items[i] != null) {
                hotbarActions.Add(inventory.items[i]);
                Debug.Log("Added " + hotbarActions[i].name + " to hotbar actions.");
            }
        }
    }


    // set-up action references for all actions
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

    // disable action references for all actions
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

    // use item 1
    private void use1(InputAction.CallbackContext context)
    {
        if (hotbarActions.Count >= 1) {
            hotbarActions[0].Use();
        }
    }

    // use item 2
    private void use2(InputAction.CallbackContext context)
    {
        if (hotbarActions.Count >= 2) {
            hotbarActions[1].Use();
        }
    }

    // use item 3
    private void use3(InputAction.CallbackContext context)
    {
        if (hotbarActions.Count >= 3) {
            hotbarActions[2].Use();
        }
    }

    // use item 4
    private void use4(InputAction.CallbackContext context)
    {
        if (hotbarActions.Count == 4) {
            hotbarActions[3].Use();
        }
    }


    // I've... definitely patched together a check here but it should be more streamlined for the actual build
    // raycasts using mouse's current position on screen to find item marked as "Interactable"
    // to be interactable, items literally have to have the "Interactable.cs" script attached to them
    // Interactable.cs can be found in the InputHandler Folder under System
    private void interactWithItem(InputAction.CallbackContext context)
    {
        Camera maincam = FindObjectOfType<Camera>();  // grab cam

        // get the mouse's current position values and cast a ray from there - 3D
        Ray ray = maincam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hitItems;

        //Debug.Log("Casting ray at: " + ray.origin);

        // on hit
        if (Physics.Raycast(ray, out hitItems, 100)) {
            Interactable interactable = hitItems.collider.GetComponent<Interactable>();
            if (interactable != null) {
                Debug.Log("Hit 3D object " + interactable.name);
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
