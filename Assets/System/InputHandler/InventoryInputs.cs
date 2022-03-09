using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
// Mace

// okay I've seriously rewritten this script like five times, it's a little rough

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
    private InputAction slot5;
    private InputAction slot6;
    private InputAction slot7;
    private InputAction slot8;

    private InputAction interact;

    // select things in the inventory - currently not hooked up but still working?
    //private InputAction clickInInventory;

    // to stop the code from reassigning inputs
    private bool unassigned = true;


    // tracking what items are in the hot bar
    [SerializeField] private List<HotbarSlot> hotbarActions;
    // singleton inventory
    Inventory inventory;
    // max hotbar length
    private int max_length;
    // hotbar parent
    [SerializeField] private Transform hotbarParent;
    // get all the slots
    private HotbarSlot[] possible_locations; 

    [SerializeField] private Transform player;

    public float interactionRadius = 0.5f;


    void Start()
    {
        // set inventory to existing instance, make list for hotbar actions, subscribe to item changes
        inventory = Inventory.instance;
        possible_locations = hotbarParent.GetComponentsInChildren<HotbarSlot>();
        inventory.onItemChangedCallback += fetchHotbarItems;
        max_length = inventory.max_hotbar_space;

        // get all the possible hotbar slots
        for (int i = 0; i < max_length; i++) {
            hotbarActions.Add(possible_locations[i]);
        }
    }


    // build list of hotbar items
    void fetchHotbarItems()
    {
        int start_pos = 0;  // position to start building at

        for (int i = 0; i < max_length; i++) {
            hotbarActions[i].ClearSlot();  // destroy the old list. we're updating.
        }

        int stop = Mathf.Min(max_length, inventory.items.Count);  // stop at max hotbar or max items in inventory
        
        // add item
        for (int i = start_pos; i < stop; i++) {
            if (inventory.items[i] != null) {
                hotbarActions[i].AddItem(inventory.items[i]);
                //Debug.Log("Added " + hotbarActions[i].name + " to hotbar actions.");
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
        
        slot5.performed -= use5;

        slot6.performed -= use6;

        slot7.performed -= use7;

        slot8.performed -= use8;

        interact.performed -= interactWithItem;

        //clickInInventory.performed -= interactWithInventory;
    }

    // only add these actions once we have a PlayerInput that exists
    void AddSetActions() {
        slot1.performed += use1;
        slot2.performed += use2;
        slot3.performed += use3;
        slot4.performed += use4;
        slot5.performed += use5;
        slot6.performed += use6;
        slot7.performed += use7;
        slot8.performed += use8;
        interact.performed += interactWithItem;
        //clickInInventory.performed += interactWithInventory;
    }

    // stolen from Blake, but updated to stop NullReference errors
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameObject.Find("InputHandler") != null)
        {
            playerInput = GameObject.Find("InputHandler").GetComponent<PlayerInput>();

            // if the action haven't been assigned yet, load them in from the PlayerInput we just found
            if (unassigned) {
                // assign all the actions
                slot1 = playerInput.actions["InputPlayer/Hotbar1"];
                slot2 = playerInput.actions["InputPlayer/Hotbar2"];
                slot3 = playerInput.actions["InputPlayer/Hotbar3"];
                slot4 = playerInput.actions["InputPlayer/Hotbar4"];
                slot5 = playerInput.actions["InputPlayer/Hotbar5"];
                slot6 = playerInput.actions["InputPlayer/Hotbar6"];
                slot7 = playerInput.actions["InputPlayer/Hotbar7"];
                slot8 = playerInput.actions["InputPlayer/Hotbar8"];
                interact = playerInput.actions["InputPlayer/Interact"];
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
            hotbarActions[0].UseItem();
        }
    }

    // use item 2
    private void use2(InputAction.CallbackContext context)
    {
        if (hotbarActions.Count >= 2) {
            hotbarActions[1].UseItem();
        }
    }

    // use item 3
    private void use3(InputAction.CallbackContext context)
    {
        if (hotbarActions.Count >= 3) {
            hotbarActions[2].UseItem();
        }
    }

    // use item 4
    private void use4(InputAction.CallbackContext context)
    {
        if (hotbarActions.Count >= 4) {
            hotbarActions[3].UseItem();
        }
    }
    
    // use item 5
    private void use5(InputAction.CallbackContext context)
    {
        if (hotbarActions.Count >= 5) {
            hotbarActions[4].UseItem();
        }
    }

    // use item 6
    private void use6(InputAction.CallbackContext context)
    {
        if (hotbarActions.Count >= 6) {
            hotbarActions[5].UseItem();
        }
    }

    // use item 7
    private void use7(InputAction.CallbackContext context)
    {
        if (hotbarActions.Count >= 7) {
            hotbarActions[6].UseItem();
        }
    }    
    
    // use item 8
    private void use8(InputAction.CallbackContext context)
    {
        if (hotbarActions.Count == 8) {
            hotbarActions[7].UseItem();
        }
    }


    // uses the postion of the EXO-Man sprite to cast a circle around and pick up items marked "Interactable"
    // to be interactable, items literally have to have the "ItemPickup.cs" script attached to them
    // this script can be found in GameHandler/InventoryHandler/Scripts
    private void interactWithItem(InputAction.CallbackContext context)
    {
        // use position to cast a circle
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(player.position, interactionRadius);
        
        // cycle through hit objects and check if they were Interactable
        for (int i = 0; i < hitObjects.Length; i++) {
            Interactable interacting = hitObjects[i].GetComponent<Interactable>();
            if (interacting != null) {
                interacting.Interact();  // interact
            }
        }

        
        /* 

        Old Raycast & Check Code, in case somebody wants to reference it

        // I've... definitely patched together a check here but it should be more streamlined for the actual build
        // raycasts using mouse's current position on screen to find item marked as "Interactable"

        Camera maincam = FindObjectOfType<Camera>();  // grab cam

        // get the mouse's current position values and cast a ray from there - 3D
        Ray ray = maincam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hitItems;

        //Debug.Log("Casting ray at: " + ray.origin);

        // on hit
        if (Physics.Raycast(ray, out hitItems, 100)) {
            Interactable interactable = hitItems.collider.GetComponent<Interactable>();
            if (interactable != null) {
                //Debug.Log("Hit 3D object " + interactable.name);
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
                //Debug.Log("Hit 2D object " + interactobj.name);
                interactobj.Interact();
            }
        }

        */
    }
}
