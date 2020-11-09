using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct bufferItem
{
    public Action action;
    public float timeOf;
}

public class PlayerInputController : MonoBehaviour
{

    PlayerControls playerControls = new PlayerControls();
    public Player player;
    public PlayerMovementController playerMovementController;
    float currTime;

    public bool readInput = true;

    bufferItem playerAttackActionBuffer = new bufferItem();

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
            playerAttackActionBuffer.timeOf = Time.time;

            // Holding activateElemental action key
            if (Input.GetKey(playerControls.activateElemental))
            {
                playerAttackActionBuffer.action = player.sword.ElementalAction;
            }
            else
            {
                playerAttackActionBuffer.action = player.sword.Action;
            }
        }
        // Right Mouse Click
        else if (Input.GetMouseButtonDown(1))
        {
            playerAttackActionBuffer.timeOf = Time.time;

            // Holding activateElemental action key
            if (Input.GetKey(playerControls.activateElemental))
            {
                playerAttackActionBuffer.action = player.selectedArtifact.ElementalAction;
            }
            else
            {
                playerAttackActionBuffer.action = player.selectedArtifact.Action;
            }
        }
    }

    void DetectArtifactSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            player.sword.elementalAttribute = ElementalAttribute.Earth;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            player.sword.elementalAttribute = ElementalAttribute.Fire;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            player.sword.elementalAttribute = ElementalAttribute.Water;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            player.sword.elementalAttribute = ElementalAttribute.Wind;
        }
    }


    private void Update()
    {
        if (readInput) {
            // execute attack actions in queue
            if (!player.AnimatorIsPlaying())
            {
                currTime = Time.time;
                if ((currTime - playerAttackActionBuffer.timeOf) <= 0.25f && playerAttackActionBuffer.action != null)
                {
                    playerAttackActionBuffer.action();
                    playerAttackActionBuffer.action = null;
                }
            }

            DetectPlayerAttackInput();
            DetectPlayerMovementInput();
            DetectArtifactSelection();
        }
    }

}
