using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Skeleton : Enemy
{

    public Collider2D skeletonCollider;
    public Collider2D attackCollider;
    bool attacking = false;


    public override IEnumerator KnockBack(Vector2 attackDirection, float force)
    {
        movementController.StopMoving();

        animator.SetFloat("HorizontalMagnitude", -attackDirection.x);
        animator.SetFloat("VerticalMagnitude", -attackDirection.y);

        Debug.Log(attackDirection);
        rigidBody.velocity = attackDirection * force;

        yield return new WaitForSeconds(0.05f);
        rigidBody.velocity = Vector2.zero;
        inputController.detectMovementInput = true;
    }


    IEnumerator Attack()
    {
        attacking = true;
        var player = Player.Instance;
        float damageAmount = 20;
        animator.SetTrigger("Attack");
        if (!attackBlocked && !player.isEthereal)
        {
            player.InflictPhysicalDamage(damageAmount * physicalAttackMultiplier * attackMultiplier);
            player.animator.SetTrigger("PlayerHurt");
            player.KnockBack(new Vector2(animator.GetFloat("HorizontalMagnitude"), animator.GetFloat("VerticalMagnitude")), 2.0f);
        }
        movementController.StopMoving();
        yield return new WaitForSeconds(0.5f);
        inputController.detectMovementInput = true;
        attacking = false;
    }

    public void AttackDetection()
    {
        if (!attacking)
        {
            // Contact filter to only include colliders in Player layer
            ContactFilter2D playerFilter = new ContactFilter2D();
            playerFilter.SetLayerMask(LayerMask.GetMask("Player"));

            // list to store all Player colliders in attack
            var playerColliders = new List<Collider2D>();

            // get all Player colliders overlapping with attack collider
            attackCollider.OverlapCollider(playerFilter, playerColliders);

            foreach (Collider2D playerCollider in playerColliders)
            {
                Player player = playerCollider.gameObject.GetComponentInParent<Player>();
                if (player != null)
                {
                    StartCoroutine(Attack());
                }
            }
        }
    }


    public void Revive()
    {
        animator.SetBool("Disabled", false);
        sortingGroup.sortingOrder = 0;
        ((SkeletonMovementController)movementController).movementMode = SkeletonMovementController.MovementMode.Patrol;
        skeletonCollider.isTrigger = false;
        health = MAX_HEALTH;
    }

    private void Update()
    {
        base.Update();
        if (inputController.detectInput && inputController.detectActionInput && health > 0)
        {
            AttackDetection();
        }
    }

}
