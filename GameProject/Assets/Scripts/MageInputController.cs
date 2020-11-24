using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageInputController : InputController
{

    public Mage mage;
    public MageMovementController movementController;


    void MageActionInputDetection()
    {

    }

    void MageMovementInputDetection()
    {
        movementController.Move();
    }

    void FixedUpdate()
    {

        if (mage.health <= 0)
        {
            return;
        }

        // Do not proceed to read player input if false
        if (!detectInput)
        {
            return;
        }

        // Detect mage action input only if enabled
        if (detectActionInput)
        {
            MageActionInputDetection();
        }

        // Detect mage movement input only if enabled
        if (detectMovementInput)
        {
            MageMovementInputDetection();
        }
    }
}
