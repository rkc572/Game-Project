using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : PlayerItem
{
    public PlayerShield(Player player) : base(player)
    {
    }

    public override void Action()
    {
        Debug.Log("Shield Bash");
        player.animator.SetBool("Shield", true);

        player.playerInputController.playerMovementController.moveDown = false;
        player.playerInputController.playerMovementController.moveUp = false;
        player.playerInputController.playerMovementController.moveLeft = false;
        player.playerInputController.playerMovementController.moveRight = false;
        
        player.properties.rigidBody.velocity = Vector2.zero;
        player.properties.damageTakenMultiplier = 0.0f;


        float colliderYoffset = 0.09f;
        Vector3 attackOffset;

        attackOffset = new Vector3(player.animator.GetFloat("HorizontalMagnitude") * 0.13f, player.animator.GetFloat("VerticalMagnitude") * 0.13f + colliderYoffset, 0.0f);
        var colliders = Physics2D.OverlapCircleAll(player.transform.position + attackOffset, 0.09f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                var enemyMob = collider.GetComponentInParent<Mob>();
                var skeleton = enemyMob.GetComponent<Skeleton>();
                if (skeleton != null && skeleton.movementMode == Skeleton.MovementMode.Disabled)
                {
                    return;
                }
                enemyMob.propertiesManager.ToggleEffectState(new RepulsedEffect(enemyMob.propertiesManager, 0.1f, new Vector2(player.animator.GetFloat("HorizontalMagnitude"), player.animator.GetFloat("VerticalMagnitude")), 3.0f));
                break;
            }
        }


    }

    protected override void EarthAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void FireAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void WaterAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void WindAction()
    {
        throw new System.NotImplementedException();
    }
}
