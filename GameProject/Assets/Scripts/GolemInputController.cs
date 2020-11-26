using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemInputController : InputController
{

    public Golem golem;
    public GolemMovementController movementController;


    void GolemActionInputDetection()
    {

    }

    void GolemMovementInputDetection()
    {
        movementController.Move();
    }

    void FixedUpdate()
    {

        if (golem.health <= 0)
        {
            return;
        }

        // Do not proceed to read player input if false
        if (!detectInput)
        {
            return;
        }

        // Detect golem action input only if enabled
        if (detectActionInput)
        {
            GolemActionInputDetection();
        }

        // Detect golem movement input only if enabled
        if (detectMovementInput)
        {
            GolemMovementInputDetection();
        }
    }
}
