using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;  // get singleton inventory

    public Transform itemsParent;

    InventorySlot[] slots;

    public GameObject inventoryUI;

    [SerializeField] private InputActionReference InventoryActions;
    
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    private void OnEnable()
    {
        InventoryActions.action.performed += toggleInventory;
        InventoryActions.action.Enable();
    }

    private void OnDisable()
    {
        InventoryActions.action.performed -= toggleInventory;
        InventoryActions.action.Disable();
    }

    private void toggleInventory(InputAction.CallbackContext context) {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    void UpdateUI() {
        Debug.Log("Updating UI.");
        for (int i = 0; i < slots.Length; i++) {
            if (i < inventory.items.Count) {
                slots[i].AddItem(inventory.items[i]);
            }
            else {
                slots[i].ClearSlot();
            }
        }
    }
}
