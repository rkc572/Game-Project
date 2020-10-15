using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{

    PlayerControls playerControls = new PlayerControls();
    public PlayerMovementController playerMovementController;

    private void DetectPlayerMovementInput()
    {
        playerMovementController.moveUp = Input.GetKey(playerControls.moveUpKey);
        playerMovementController.moveDown = Input.GetKey(playerControls.moveDownKey);
        playerMovementController.moveLeft = Input.GetKey(playerControls.moveLeftKey);
        playerMovementController.moveRight = Input.GetKey(playerControls.moveRightKey);
    }

    private void DetectPlayerAttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attacked!");
        }
    }

    private void Update()
    {
        DetectPlayerMovementInput();
        DetectPlayerAttackInput();
    }

}
