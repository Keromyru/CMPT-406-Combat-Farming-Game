using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Written by Blake Williams

// Allows the player to be moved
public class PlayerMovement : MonoBehaviour
{
    // the player object
    private Rigidbody player;

    // The inputs from the input system
    private PlayerInput playerInput;
    public GameObject inputHandler;

    // The speed of the player
    public float speed;

    private void Awake()
    {
        player = GetComponent<Rigidbody>();

        // Allows you to use the player input
        playerInput = inputHandler.GetComponent<PlayerInput>();
    }


    private void FixedUpdate()
    {
        // Gets the movement input and applies a constant velocity to the player
        Vector2 inputVector = playerInput.actions["PlayerMovement"].ReadValue<Vector2>();
        // velocity is incredibly buggy and unpredictable. Instead use move position
        // player.velocity = new Vector3(inputVector.x, 0, inputVector.y) * speed;

        player.MovePosition(player.position + new Vector3(inputVector.x, 0, inputVector.y) * speed);

        // Old code, allows for acceleration movement using forces
        /*player.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);*/
    }


    // Alternate not working version that uses events (have to click buttons multiple times to move)
    /*    private Rigidbody player;
        private InputActions inputActions;

        private void Awake()
        {
            player = GetComponent<Rigidbody>();

            inputActions = new InputActions();
            inputActions.InputPlayer.Enable();
        }

        public void Move()
        {
            Debug.Log("moved");
            Vector2 inputVector = inputActions.InputPlayer.Movement.ReadValue<Vector2>();
            float speed = 5f;
            player.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
        }*/
}
