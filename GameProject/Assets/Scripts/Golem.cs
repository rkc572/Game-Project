using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Enemy
{

    bool dead = false;
    bool attacking = false;
    public GolemSounds sounds;

    public Collider2D attackCollider;

    public GameObject attackParticles;

    public override IEnumerator KnockBack(Vector2 attackDirection, float force)
    {
        movementController.StopMoving();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator.SetFloat("HorizontalMagnitude", -attackDirection.x);
        animator.SetFloat("VerticalMagnitude", -attackDirection.y);

        Debug.Log(attackDirection);
        rigidBody.velocity = attackDirection * force;

        yield return new WaitForSeconds(0.05f);
        rigidBody.velocity = Vector2.zero;
        inputController.detectMovementInput = true;
    }

    void UpdateWalkingAnimatorParameters()
    {


        var scaledDirection = rigidBody.velocity;
    /*
        if (scaledDirection.y > 0)
        {
            scaledDirection.y = 1;
        }
        else if (scaledDirection.y < 0)
        {
            scaledDirection.y = -1;
        }

        if (scaledDirection.x > 0)
        {
            scaledDirection.x = 1;
        }
        else if (scaledDirection.x < 0)
        {
            scaledDirection.x = -1;
        }

    */
        animator.SetFloat("HorizontalMagnitude", scaledDirection.x);
        animator.SetFloat("VerticalMagnitude", scaledDirection.y);
        animator.SetBool("Moving", true);
    }

    IEnumerator Die()
    {

        sortingGroup.sortingOrder = -1;
        rigidBody.velocity = Vector2.zero;
        gameObject.GetComponent<Collider2D>().enabled = false;

        animator.SetTrigger("Disable");
        animator.SetBool("Disabled", true);
        sounds.PlayGolemDeathSound();

        yield return new WaitForSeconds(1.0f);

        var sprite = gameObject.GetComponent<SpriteRenderer>();

        for (int i = 0; i < 100; i++)
        {
            sprite.material.color = new Color(sprite.material.color.r, sprite.material.color.g, sprite.material.color.b, sprite.material.color.a - 0.01f);
            yield return new WaitForSeconds(0.02f);
        }

        Destroy(transform.parent.gameObject, 1.0f);
    }


    IEnumerator Attack()
    {

        yield return new WaitForSeconds(2.0f);
        while (health > 0)
        {
            movementController.StopMoving();
            animator.SetTrigger("Attack");
            attackParticles.SetActive(true);
            GroundPound();
            yield return new WaitForSeconds(0.5f);
            attackParticles.SetActive(false);
            yield return new WaitForSeconds(2.0f);
        }
    }

    public void GroundPound()
    {
        float damageAmount = 300.0f;

        if (!attacking)
        {
            sounds.PlayGolemAttackSound();


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
                    if (!player.isEthereal)
                    {
                        player.InflictPhysicalDamage(damageAmount * physicalAttackMultiplier * attackMultiplier);
                        player.animator.SetTrigger("PlayerHurt");
                        StartCoroutine(player.KnockBack(new Vector2(animator.GetFloat("HorizontalMagnitude"), animator.GetFloat("VerticalMagnitude")), 5.0f));
                    }
                }
            }

            inputController.detectMovementInput = true;
        }
    }

    private void Start()
    {
        inputController.detectInput = true;
        inputController.detectMovementInput = true;
        inputController.detectActionInput = true;
        StartCoroutine(Attack());
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (health > 0)
        {
            UpdateWalkingAnimatorParameters();
        }
        else if (!dead)
        {
            dead = true;
            inputController.detectInput = false;
            StartCoroutine(Die());
        }
    }
}
