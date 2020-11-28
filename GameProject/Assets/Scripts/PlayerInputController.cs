using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : InputController
{
    public Player player;
    public PlayerMovementController movementController;

    // Start is called before the first frame update
    void Start()
    {
    }

    void PlayerItemActionInputDetection()
    {
        // Detect sword input
        player.playerSword.DetectInput();

        // Detect artifact input
        if (player.selectedArtifact != null)
            player.selectedArtifact.DetectInput();
    }

    void PlayerMovementInputDetection()
    {
        // Detect player movement input
        movementController.DetectInput();
    }
    
    void PotionInputDetection()
    {

        if (player.potions.Count >= 1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // shift list to the left
                player.potions.Add(player.potions[0]);
                player.potions.RemoveAt(0);

                // update selected potion
                player.selectedPotion = player.potions[player.potions.Count / 2];

            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                // shift list to the right
                player.potions.Insert(0, player.potions[player.potions.Count - 1]);
                player.potions.RemoveAt(player.potions.Count - 1);

                // update selected potion
                player.selectedPotion = player.potions[player.potions.Count / 2];

            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                player.selectedPotion.Consume();

                // remove potion once runs out
                if (player.selectedPotion.quantity <= 0)
                {
                    player.potions.Remove(player.selectedPotion);
                    // update selected potion
                    if (player.potions.Count >= 1)
                    {
                        player.selectedPotion = player.potions[player.potions.Count / 2];
                    }
                    else
                    {
                        player.selectedPotion = null;
                    }
                }
                /*
                print("POTIONS: ");
                foreach (Potion invPot in Player.Instance.potions)
                {
                    var name = invPot.GetType().Name;
                    var quant = invPot.quantity;
                    print($"{name} {quant}");
                }

                print($"Selected potion: {Player.Instance.selectedPotion.GetType().Name} {Player.Instance.selectedPotion.quantity}");
                */
            }
        }

    }

    void PlayerItemSwapInput()
    {
        int index = -1;

        // Slot 1 picked
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            index = 0;
        }

        // Slot 2 picked
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            index = 1;
        }

        // Slot 3 picked
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            index = 2;
        }

        // Slot 4 picked
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            index = 3;
        }

        // end function if no artifact selected
        if (index == -1)
        {
            return;
        }

        //verify slot exists
        if (player.artifacts.Count >= index + 1)
        {
            player.selectedArtifact = player.artifacts[index];
        }
        else
        {

        }
    }


    // Update is called once per frame
    void Update()
    {

        // Do not proceed to read player input if false
        if (!detectInput)
        {
            return;
        }

        // Detect player item action input only if enabled
        if (detectActionInput)
        {
            PlayerItemActionInputDetection();
        }

        // Detect player movement input only if enabled
        if (detectMovementInput)
        {
            PlayerMovementInputDetection();
        }

        // Detect potion input
        PotionInputDetection();

        // Detect player artifact swap input
        PlayerItemSwapInput();
    }
}
