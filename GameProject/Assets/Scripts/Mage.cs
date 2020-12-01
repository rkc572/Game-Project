using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Mage : Enemy
{

    bool dead = false;

    public override IEnumerator KnockBack(Vector2 attackDirection, float force)
    {
        movementController.StopMoving();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        //animator.SetFloat("HorizontalMagnitude", -attackDirection.x);
        //animator.SetFloat("VerticalMagnitude", -attackDirection.y);

        Debug.Log(attackDirection);
        rigidBody.velocity = attackDirection * force;

        yield return new WaitForSeconds(0.05f);
        rigidBody.velocity = Vector2.zero;
        inputController.detectMovementInput = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Attack());
        StartCoroutine(RangeAttack());
    }


    IEnumerator RangeAttack()
    {
        while (health > 0)
        {
            if (!dead && inputController.detectActionInput && inputController.detectInput)
            {
                rigidBody.velocity = Vector2.zero;
                Vector3 playerOffset = new Vector3(0.0f, 0.1f, 0.0f);
                Vector3 target = (Player.Instance.transform.position + playerOffset);

                var projectilePrefab = (MageProjectile)Resources.Load("prefabs/MageProjectile", typeof(MageProjectile));
                var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                Vector2 targetDirection = target - projectile.transform.position;
                Vector2 newVelocity = targetDirection.normalized * 1.75f;

                projectile.rigidBody.velocity = newVelocity;
                projectile.mage = this;
            }
            yield return new WaitForSeconds(6.0f);
        }
    }


    IEnumerator Attack()
    {
        while (health > 0) {
            if (!dead && inputController.detectActionInput && inputController.detectInput)
            {
                animator.SetTrigger("Attack");
            }
            yield return new WaitForSeconds(8.0f);
        }
    }


    void UpdateWalkingAnimatorParameters()
    {
        var scaledDirection = rigidBody.velocity;

        if (scaledDirection.y > speed / 2)
        {
            scaledDirection.y = 1;
        }
        else if (scaledDirection.y < -1 * (speed / 2))
        {
            scaledDirection.y = -1;
        }
        else
        {
            scaledDirection.y = 0;
        }

        if (scaledDirection.x > speed / 2)
        {
            scaledDirection.x = 1;
        }
        else if (scaledDirection.x < -1 * (speed / 2))
        {
            scaledDirection.x = -1;
        }
        else
        {
            scaledDirection.x = 0;
        }

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

        yield return new WaitForSeconds(1.0f);

        var sprite = gameObject.GetComponent<SpriteRenderer>();

        for (int i = 0; i < 100; i++)
        {
            sprite.material.color = new Color(sprite.material.color.r, sprite.material.color.g, sprite.material.color.b, sprite.material.color.a - 0.01f);
            yield return new WaitForSeconds(0.02f);
        }

        Destroy(transform.parent.gameObject, 1.0f);
    }

    public void CircleAttack()
    {
        var circleAttackPrefab = (GameObject)Resources.Load("prefabs/MageCircleAttack", typeof(GameObject));
        if (FindObjectOfType<MageCircleAttack>() == null)
        {
            GameObject.Instantiate(circleAttackPrefab, transform.parent.position - new Vector3(0.0f, 0.25f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (health > 0)
        {
            UpdateWalkingAnimatorParameters();
        }
        else if(!dead)
        {
            dead = true;
            inputController.detectInput = false;
            StartCoroutine(Die());
        }
    }

}
