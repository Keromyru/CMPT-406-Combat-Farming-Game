using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Written by Blake Williams

// Controls the enabling and disabling of input action maps. As well as holding the main input actions variable needed
public class InputController : MonoBehaviour
{
    // The inputs from the input system
    private PlayerInput playerInput;

    // The last enabled action map
    private string lastActionMap;

    private void Awake()
    {
        // Sets the first action map enabled to player
        lastActionMap = "Player";
        playerInput = GetComponent<PlayerInput>();
    }


    private void OnEnable()
    {
        // Inputs to open/close menu
        playerInput.actions["OpenMenuFromPlayer"].performed += EnableMenuFromPlayer;
        playerInput.actions["OpenMenuFromUI"].performed += EnableMenuFromUI;
        playerInput.actions["CloseMenu"].performed += DisableMenu;

        // Inputs to open/close UI/Inventory
        playerInput.actions["OpenInventory"].performed += EnableInventory;
        playerInput.actions["CloseInventory"].performed += DisableInventory;


/*        // Tests
        playerInput.actions["LeftClickMenu"].performed += testmenu;
        playerInput.actions["Hotbar1"].performed += testplayer;
        playerInput.actions["EButton"].performed += testui;*/
    }

    private void OnDisable()
    {
        // Inputs to open/close menu
        playerInput.actions["OpenMenuFromPlayer"].performed -= EnableMenuFromPlayer;
        playerInput.actions["OpenMenuFromUI"].performed -= EnableMenuFromUI;
        playerInput.actions["CloseMenu"].performed -= DisableMenu;

        // Inputs to open/close UI/Inventory
        playerInput.actions["OpenInventory"].performed -= EnableInventory;
        playerInput.actions["CloseInventory"].performed -= DisableInventory;
    }


    // Disables the menu action map and enables the last action map used before
    private void DisableMenu(InputAction.CallbackContext context)
    {
        if (lastActionMap == "Player")
        {
            playerInput.SwitchCurrentActionMap("InputPlayer");
        }
        else
        {
            playerInput.SwitchCurrentActionMap("InputUI");
        }
    }

    // Enables the UI action map
    private void EnableInventory(InputAction.CallbackContext context)
    {
        playerInput.SwitchCurrentActionMap("InputUI");
    }

    // Disables the UI action map
    private void DisableInventory(InputAction.CallbackContext context)
    {
        playerInput.SwitchCurrentActionMap("InputPlayer");
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
        playerInput.SwitchCurrentActionMap("InputMenu");
    }


 /*   // All test functions
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
        Debug.Log("E button in ui");
    }*/
}
