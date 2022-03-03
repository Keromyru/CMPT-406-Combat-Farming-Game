using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Mace

// manages Inventory and Hotbar UI displays
public class InventoryUI : MonoBehaviour
{
    Inventory inventory;  // get singleton inventory
    public Transform itemsParent;  // the parent of all InventorySlots
    public Transform hotbarParent;  // the parent of all HotbarSlots
    InventorySlot[] slots;  // array of Inventory slots
    HotbarSlot[] hotbarSlots;  // array of Hotbar slots
    public int maxHotbarSlots = 4;  // maximum number of hotbar slots

    public GameObject inventoryUI;  // reference to the actual GameObject

    // input handling
    private PlayerInput playerInput;
    private InputAction openInventory;
    private InputAction closeInventory;
    private bool unassigned = true;  // used to see if the InputActions have been properly assigned
    

    // Start is called before the first frame update
    void Start()
    {
        // grab singleton & subscribe to changes in items
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        // fill arrays with all the slots on scene
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        hotbarSlots = hotbarParent.GetComponentsInChildren<HotbarSlot>();
    }

    // input stuff to toggle inventory visibility. hotbar doesn't toggle.
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // input stuff to toggle inventory visibility. hotbar doesn't toggle.
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        openInventory.performed -= viewInventory;
        closeInventory.performed -= hideInventory;
    }

    // input stuff to toggle inventory visibility. hotbar doesn't toggle.
    private void viewInventory(InputAction.CallbackContext context) {
        //Debug.Log("Opening inventory.");
        inventoryUI.SetActive(true);
    }

    // input stuff to toggle inventory visibility. hotbar doesn't toggle.
    private void hideInventory(InputAction.CallbackContext context) {
        inventoryUI.SetActive(false);
        //Debug.Log("Closing inventory.");
    }

    // add in the actions for these inputs only AFTER they've been set-up
    void AddSetActions() {
        openInventory.performed += viewInventory;
        closeInventory.performed += hideInventory;
    }

    // stolen from Blake's PlayerInput set-up guide
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (GameObject.Find("InputHandler") != null) {
            playerInput = GameObject.Find("InputHandler").GetComponent<PlayerInput>();

            // checks to see if the inputs are unassigned & assigns them now that we have PlayerInput set-up
            if (unassigned) {
                openInventory = playerInput.actions["InputPlayer/OpenInventory"];
                closeInventory = playerInput.actions["InputUI/CloseInventory"];
                unassigned = false;
                AddSetActions();
            }
        }
    }

    // update UI for hotbar & inventory
    void UpdateUI() {

        //Debug.Log("Updating Inventory + Hotbar UI.");

        // cycle through all slots, add item if one exists in our inventory
        for (int i = 0; i < slots.Length; i++) {
            if (i < inventory.items.Count) {
                slots[i].AddItem(inventory.items[i]);

                // add it to our hotbar too if there's room
                if (i < maxHotbarSlots) {
                    hotbarSlots[i].AddItem(inventory.items[i]);
                }
            }
            // if there isn't room, wipe the slot back to empty state
            else {
                slots[i].ClearSlot();

                // same deal in the hotbar
                if (i < maxHotbarSlots) {
                    hotbarSlots[i].ClearSlot();
                }
            }
        }
    }
}
