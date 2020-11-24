using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonInputController : InputController
{

    public Skeleton skeleton;
    public SkeletonMovementController movementController;

    // Start is called before the first frame update
    void Start()
    {
    }

    void SkeletonActionInputDetection()
    {

    }

    void SkeletonMovementInputDetection()
    {
        movementController.Move();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Do not proceed to read player input if false
        if (!detectInput)
        {
            return;
        }

        // Detect skeleton action input only if enabled
        if (detectActionInput)
        {
            SkeletonActionInputDetection();
        }

        // Detect skeleton movement input only if enabled
        if (detectMovementInput)
        {
            SkeletonMovementInputDetection();
        }
    }
}
