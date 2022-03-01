using UnityEngine;
using UnityEngine.InputSystem;

// Mace

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;  // get singleton inventory

    public Transform itemsParent;  // the parent of all InventorySlots

    public Transform hotbarParent;  // the parent of all HotbarSlots

    InventorySlot[] slots;  // array of Inventory slots

    HotbarSlot[] hotbarSlots;  // array of Hotbar slots
    public int maxHotbarSlots = 4;  // maximum number of hotbar slots

    public GameObject inventoryUI;  // reference to the actual GameObject

    // again, all my input stuff probably has to be updated. but this one is easy.
    [SerializeField] private InputActionReference InventoryActions;
    
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
        InventoryActions.action.performed += toggleInventory;
        InventoryActions.action.Enable();
    }

    // input stuff to toggle inventory visibility. hotbar doesn't toggle.
    private void OnDisable()
    {
        InventoryActions.action.performed -= toggleInventory;
        InventoryActions.action.Disable();
    }

    // input stuff to toggle inventory visibility. hotbar doesn't toggle.
    private void toggleInventory(InputAction.CallbackContext context) {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    // update UI for hotbar & inventory
    void UpdateUI() {

        Debug.Log("Updating Inventory + Hotbar UI.");

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
