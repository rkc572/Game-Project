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
        animator.SetFloat("HorizontalMagnitude", -attackDirection.x);
        animator.SetFloat("VerticalMagnitude", -attackDirection.y);

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
        GameObject.Instantiate(circleAttackPrefab, transform.parent.position - new Vector3(0.0f, 0.25f), Quaternion.identity);
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
