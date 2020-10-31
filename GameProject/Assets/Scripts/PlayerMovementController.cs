using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Mathematics;

public class PlayerMovementController : MonoBehaviour
{
    public Player player;

    public bool moveUp, moveDown, moveLeft, moveRight = false;



    Vector2 moveVelocity = Vector2.zero;

    private void Move()
    {
        float verticalVelocity = ((moveUp ? 1 : 0) + (moveDown ? -1 : 0));
        float horizontalVelocity = ((moveRight ? 1 : 0) + (moveLeft ? -1 : 0));

        Vector2 newVelocity = new Vector2(horizontalVelocity, verticalVelocity).normalized * player.properties.speed;

        Vector2 smoothenedVelocity = Vector2.SmoothDamp(player.properties.rigidBody.velocity, newVelocity, ref moveVelocity, 0.2f);

        newVelocity = new Vector2(math.abs(horizontalVelocity) == 1 ? smoothenedVelocity.x : 0.0f, math.abs(verticalVelocity) == 1 ? smoothenedVelocity.y : 0.0f);

        bool playerIsMoving = moveUp || moveDown || moveLeft || moveRight;

        player.animator.SetBool("Moving", playerIsMoving);

        if (playerIsMoving)
        {
            player.properties.rigidBody.velocity = newVelocity;
            player.animator.SetFloat("HorizontalMagnitude", horizontalVelocity);
            player.animator.SetFloat("VerticalMagnitude", verticalVelocity);
        }
        else
        {
            player.properties.rigidBody.velocity = Vector2.SmoothDamp(player.properties.rigidBody.velocity, Vector2.zero, ref moveVelocity, 0.02f);
        }

        if (player.animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            player.animator.SetFloat("MoveAnimationSpeedMultiplier", player.properties.rigidBody.velocity.magnitude);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

}
