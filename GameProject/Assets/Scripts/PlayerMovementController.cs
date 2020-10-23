using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Player player;

    public bool moveUp, moveDown, moveLeft, moveRight = false;

    private void Move()
    {
        float verticalVelocity = ((moveUp ? 1 : 0) + (moveDown ? -1 : 0));
        float horizontalVelocity = ((moveRight ? 1 : 0) + (moveLeft ? -1 : 0));

        Vector2 newVelocity = new Vector2(horizontalVelocity, verticalVelocity);
        newVelocity = newVelocity.normalized * player.properties.speed;
        player.properties.rigidBody.velocity = newVelocity;

        bool playerIsMoving = newVelocity != Vector2.zero;

        player.animator.speed = player.properties.speed;
        player.animator.SetBool("Moving", playerIsMoving);
        if (playerIsMoving)
        {
            player.animator.SetFloat("HorizontalMagnitude", horizontalVelocity);
            player.animator.SetFloat("VerticalMagnitude", verticalVelocity);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

}
