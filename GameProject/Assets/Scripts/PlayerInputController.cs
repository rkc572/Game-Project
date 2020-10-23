using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{

    PlayerControls playerControls = new PlayerControls();
    public Player player;
    public PlayerMovementController playerMovementController;

    Queue<Action> playerAttackActionBuffer = new Queue<Action>();

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
        // Left Mouse Click
        if (Input.GetMouseButtonDown(0))
        {
            // Holding activateElemental action key
            if (Input.GetKey(playerControls.activateElemental))
            {
                playerAttackActionBuffer.Enqueue(player.sword.ElementalAction);
            }
            else
            {
                playerAttackActionBuffer.Enqueue(player.sword.Action);
            }
        }
        // Right Mouse Click
        else if (Input.GetMouseButtonDown(1))
        {
            // Holding activateElemental action key
            if (Input.GetKey(playerControls.activateElemental))
            {
                playerAttackActionBuffer.Enqueue(player.selectedArtifact.ElementalAction);
            }
            else
            {
                playerAttackActionBuffer.Enqueue(player.selectedArtifact.Action);
            }
        }
    }


    private void Update()
    {
        // execute attack actions in queue
        if (playerAttackActionBuffer.Count > 0 && !player.AnimatorIsPlaying())
        {
            playerAttackActionBuffer.Dequeue()();
        }

        DetectPlayerAttackInput();
        DetectPlayerMovementInput();
    }

}
