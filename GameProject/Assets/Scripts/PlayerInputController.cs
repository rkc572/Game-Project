using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{

    PlayerControls playerControls = new PlayerControls();
    public Player player;
    public PlayerMovementController playerMovementController;

    private void DetectPlayerMovementInput()
    {
        if (!player.AnimatorIsPlaying())
        {
            playerMovementController.moveUp = Input.GetKey(playerControls.moveUpKey);
            playerMovementController.moveDown = Input.GetKey(playerControls.moveDownKey);
            playerMovementController.moveLeft = Input.GetKey(playerControls.moveLeftKey);
            playerMovementController.moveRight = Input.GetKey(playerControls.moveRightKey);
        }
        else
        {
            playerMovementController.moveUp = false;
            playerMovementController.moveDown = false;
            playerMovementController.moveLeft = false;
            playerMovementController.moveRight = false;
        }
    }

    private void DetectPlayerAttackInput()
    {
        if (!player.AnimatorIsPlaying())
        {
            // Left Mouse Click
            if (Input.GetMouseButtonDown(0))
            {
                // Holding activateElemental action key
                if (Input.GetKey(playerControls.activateElemental))
                {
                    player.sword.ElementalAction();
                }
                else
                {
                    player.sword.Action();
                }
            }
            // Right Mouse Click
            else if (Input.GetMouseButtonDown(1))
            {
                // Holding activateElemental action key
                if (Input.GetKey(playerControls.activateElemental))
                {
                    player.selectedArtifact.ElementalAction();
                }
                else
                {
                    player.selectedArtifact.Action();
                }
            }
        }
    }

    private void Update()
    {
        DetectPlayerAttackInput();
        DetectPlayerMovementInput();
    }

}
