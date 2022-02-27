using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Written by Blake Williams

// Controls the enabling and disabling of input action maps. As well as holding the main input actions variable needed
public class InputController : MonoBehaviour
{
    // The inputs from the input system (grab this variable for inputs)
    public InputActions inputActions;
    
    // The last enabled action map
    private string lastActionMap;

    private void Awake()
    {
        // The original input action 
        inputActions = new InputActions();

        // Sets the first action map enabled to player
        lastActionMap = "Player";
        inputActions.InputPlayer.Enable();
    }


    private void OnEnable()
    {
        // Inputs to open/close menu
        inputActions.InputPlayer.Menu.performed += EnableMenuFromPlayer;
        inputActions.InputUI.Menu.performed += EnableMenuFromUI;
        inputActions.InputMenu.Menu.performed += DisableMenu;

        // Inputs to open/close UI/Inventory
        inputActions.InputPlayer.Inventory.performed += EnableInventory;
        inputActions.InputUI.Inventory.performed += DisableInventory;


        // Tests
        inputActions.InputMenu.PrimaryAction.performed += testmenu;
        inputActions.InputPlayer.Hotbar1.performed += testplayer;
        inputActions.InputUI.Hotbar1.performed += testui;
    }

    private void OnDisable()
    {
        // Inputs to open/close menu
        inputActions.InputPlayer.Menu.performed -= EnableMenuFromPlayer;
        inputActions.InputUI.Menu.performed -= EnableMenuFromUI;
        inputActions.InputMenu.Menu.performed -= DisableMenu;

        // Inputs to open/close UI/Inventory
        inputActions.InputPlayer.Inventory.performed -= EnableInventory;
        inputActions.InputUI.Inventory.performed -= DisableInventory;
    }


    // Disables the menu action map and enables the last action map used before
    private void DisableMenu(InputAction.CallbackContext context)
    {
        inputActions.InputMenu.Disable();

        if (lastActionMap == "Player")
        {
            DisableInventory(context);
        }
        else
        {
            EnableInventory(context);
        }
    }

    // Enables the UI action map
    private void EnableInventory(InputAction.CallbackContext context)
    {
        inputActions.InputPlayer.Disable();
        inputActions.InputUI.Enable();
    }

    // Disables the UI action map
    private void DisableInventory(InputAction.CallbackContext context)
    {
        inputActions.InputUI.Disable();
        inputActions.InputPlayer.Enable();
    }


    // Enables the menu action map and saving that the last mapping was player
    private void EnableMenuFromPlayer(InputAction.CallbackContext context)
    {
        lastActionMap = "Player";
        EnableMenu();
    }

    // Enables the menu action map and saving that the last mapping was UI
    private void EnableMenuFromUI(InputAction.CallbackContext context)
    {
        lastActionMap = "UI";
        EnableMenu();
    }

    // Enables the menu action map
    private void EnableMenu()
    {
        inputActions.InputUI.Disable();
        inputActions.InputPlayer.Disable();
        inputActions.InputMenu.Enable();
    }


    // All test functions
    private void testmenu(InputAction.CallbackContext context)
    {
        Debug.Log("left click in menu");
    }
    private void testplayer(InputAction.CallbackContext context)
    {
        Debug.Log("hotbar1 in player");
    }

    private void testui(InputAction.CallbackContext context)
    {
        Debug.Log("hotbar1 in ui");
    }
}
