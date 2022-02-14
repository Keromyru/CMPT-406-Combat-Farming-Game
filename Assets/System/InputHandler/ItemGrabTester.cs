using UnityEngine;
using UnityEngine.InputSystem;

// IDEALLY - this whole script gets thrown out and this just gets added to the Player Controller
public class ItemGrabTester : MonoBehaviour
{
    // in the actual player controller script, you would then just check if mouse down input
    // collided with an interactable via raycasting...
    // (video link: https://www.youtube.com/watch?v=9tePzyL6dgc @ 2:15)
    // and a link to the forum post I found while trying to do the mouse bit accoording to the new input system
    // https://forum.unity.com/threads/mouse-position-with-new-input-system.829248/
    
    // I've patched together a check here but it should be more streamlined for the actual build

    // Figuring out how to actually subscribe and activate an event came from here:
    // https://www.youtube.com/watch?v=zIhtPSX8hqA

    [SerializeField] private InputActionReference selectItem;

    private void OnEnable()
    {
        selectItem.action.performed += selectingItem;
        selectItem.action.Enable();
    }

    private void OnDisable()
    {
        selectItem.action.performed -= selectingItem;
        selectItem.action.Disable();
    }

    private void selectingItem(InputAction.CallbackContext context)
    {
        Camera maincam = FindObjectOfType<Camera>();

        // get the mouse's current position values and cast a ray from there
        Ray ray = maincam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hitItems;

        Debug.Log("Casting ray at: " + ray.origin);

        // on hit
        if (Physics.Raycast(ray, out hitItems, 100)) {
            Interactable interactable = hitItems.collider.GetComponent<Interactable>();
            if (interactable != null) {
                Debug.Log("Hit object " + interactable.name + " with right click");
                interactable.Interact();
            }
        }
    }
}
