using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
// Mace

// okay've seriously rewritten this script like five times, it's a little rough

// this is currently attached to the InventoryHandler (prefab)
public class InventoryInputs : MonoBehaviour
{
    // adding input
    private PlayerInput playerInput;

    // out of inventory actions
    private InputAction slot1;
    private InputAction slot2;
    private InputAction slot3;
    private InputAction slot4;
    private InputAction click;

    // select things in the inventory
    //private InputAction clickInInventory;

    // to stop the code from reassigning inputs
    private bool unassigned = true;


    // tracking what items are in the hot bar
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
    // will need to changed to work with button selection (like to show what you've selected) in hotbar, but it's functional
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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // disable action references for all actions
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        
        slot1.performed -= use1;

        slot2.performed -= use2;

        slot3.performed -= use3;

        slot4.performed -= use4;

        click.performed -= interactWithItem;

        //clickInInventory.performed -= interactWithInventory;
    }

    void AddSetActions() {
        slot1.performed += use1;
        slot2.performed += use2;
        slot3.performed += use3;
        slot4.performed += use4;
        click.performed += interactWithItem;
        //clickInInventory.performed += interactWithInventory;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameObject.Find("InputHandler") != null)
        {
            playerInput = GameObject.Find("InputHandler").GetComponent<PlayerInput>();

            if (unassigned) {
                // assign all the actions
                slot1 = playerInput.actions["InputPlayer/Hotbar1"];
                slot2 = playerInput.actions["InputPlayer/Hotbar2"];
                slot3 = playerInput.actions["InputPlayer/Hotbar3"];
                slot4 = playerInput.actions["InputPlayer/Hotbar4"];
                click = playerInput.actions["InputPlayer/PrimaryAction"];
                //clickInInventory = playerInput.actions["InputUI/LeftClickUI"];
                unassigned = false;
                AddSetActions();
            }
        }
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
    // to be interactable, items literally have to have the "ItemPickup.cs" script attached to them
    // this script can be found in GameHandler/InventoryHandler/Scripts
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

    //private void interactWithInventory(InputAction.CallbackContext context)
    //{
        //Debug.Log("Clicking while in UI.");
    //}
}
