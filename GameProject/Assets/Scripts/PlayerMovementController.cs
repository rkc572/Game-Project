using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MovementController
{
    public Player player;
    bool moveUp, moveDown, moveLeft, moveRight = false;
    Vector2 moveVelocity = Vector2.zero;

    public override void StopMoving()
    {
        moveUp = false;
        moveDown = false;
        moveLeft = false;
        moveRight = false;
        player.rigidBody.velocity = Vector2.zero;
    }

    public void DetectInput()
    {
        moveUp = Input.GetKey(KeyCode.W);
        moveLeft = Input.GetKey(KeyCode.A);
        moveDown = Input.GetKey(KeyCode.S);
        moveRight = Input.GetKey(KeyCode.D);
    }

    void Move()
    {
        float verticalVelocity = ((moveUp ? 1 : 0) + (moveDown ? -1 : 0));
        float horizontalVelocity = ((moveRight ? 1 : 0) + (moveLeft ? -1 : 0));

        Vector2 newVelocity = new Vector2(horizontalVelocity, verticalVelocity).normalized * player.speed;

        Vector2 smoothenedVelocity = Vector2.SmoothDamp(player.rigidBody.velocity, newVelocity, ref moveVelocity, 0.2f);

        newVelocity = new Vector2(Mathf.Abs(horizontalVelocity) == 1 ? smoothenedVelocity.x : 0.0f, Mathf.Abs(verticalVelocity) == 1 ? smoothenedVelocity.y : 0.0f);

        bool playerIsMoving = newVelocity != Vector2.zero;

        player.animator.SetBool("Moving", playerIsMoving);

        if (playerIsMoving)
        {
            player.rigidBody.velocity = newVelocity;
            player.animator.SetFloat("HorizontalMagnitude", horizontalVelocity);
            player.animator.SetFloat("VerticalMagnitude", verticalVelocity);
        }
        else
        {
            player.rigidBody.velocity = Vector2.SmoothDamp(player.rigidBody.velocity, Vector2.zero, ref moveVelocity, 0.02f);
        }

        if (player.animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            player.animator.SetFloat("MoveAnimationSpeedMultiplier", player.rigidBody.velocity.magnitude);
        }
    }

    private void FixedUpdate()
    {
        if (player.inputController.detectMovementInput)
        {
            Move();
        }
    }
}
