using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMovementController : MovementController
{

    public Golem golem;

    Vector2 smoothVelocityReference = Vector2.zero;

    public override void StopMoving()
    {
        golem.inputController.detectMovementInput = false;
        golem.rigidBody.velocity = Vector2.zero;
    }

    public void Move()
    {
        var player = Player.Instance;


        if (player.isEthereal)
        {
            golem.rigidBody.velocity = Vector2.zero;
            return;
        }

        Vector3 playerOffset = new Vector3(0.0f, 0.1f, 0.0f);
        Vector3 target = (player.transform.position + playerOffset);
        Vector2 targetDirection = target - transform.position;
        Vector2 newVelocity = targetDirection.normalized * golem.speed;

        golem.rigidBody.velocity = newVelocity;
    }

}
