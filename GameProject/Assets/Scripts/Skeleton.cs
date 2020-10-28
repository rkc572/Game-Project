using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public enum Mode
    {
        Sentry,
        Pursuit,
        Attack,
        Disabled,
        Dead
    };

    public Mob properties;
    public Animator animator;
    public float goldDropOnDeath = 1.0f;
    public float sightRadius = 0.4f;
    public float attackRadius = 0.15f;

    public Mode movementMode = Mode.Sentry;

    Player playerReference;


    // Sentry Mode variables;
    bool patrolVertical = false;
    bool patrolHorizontal = true;
    bool up, down, left, right = false;
    float minHorizontalMovement = 1.0f;
    float maxHorizontalMovement = 1.0f;
    float minVerticalMovement = 1.0f;
    float maxVerticalMovement = 1.0f;
    Vector3 startingPosition;


    Vector2 smoothVelocityReference = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        // initialize starting movement direction
        right = true;
        // Assign random range of gold to drop upon death
        goldDropOnDeath = Random.Range(0.0f, 15.0f);
        // Record initial starting position
        startingPosition = transform.position;
    }

    void Patrol()
    {
        if (transform.position.x >= startingPosition.x + maxHorizontalMovement)
        {
            right = false;
            left = true;
        }
        else if (transform.position.x <= startingPosition.x - minHorizontalMovement)
        {
            right = true;
            left = false;
        }

        if (transform.position.y >= startingPosition.y + maxVerticalMovement)
        {
            up = false;
            down = true;
        }
        else if (transform.position.y <= startingPosition.y - minVerticalMovement)
        {
            up = true;
            down = false;
        }

        if (patrolHorizontal)
        {
            if (right)
            {
                properties.rigidBody.velocity = new Vector2(1.0f, 0.0f) * properties.speed;
            }
            else if (left)
            {
                properties.rigidBody.velocity = new Vector2(-1.0f, 0.0f) * properties.speed;
            }
        }
        else if (patrolVertical)
        {
            if (up)
            {
                properties.rigidBody.velocity = new Vector2(0.0f, 1.0f) * properties.speed;
            }
            else if (down)
            {
                properties.rigidBody.velocity = new Vector2(0.0f, -1.0f) * properties.speed;
            }
        }

        animator.SetFloat("HorizontalMagnitude", properties.rigidBody.velocity.x);
        animator.SetFloat("VerticalMagnitude", properties.rigidBody.velocity.y);
        animator.SetBool("Moving", properties.rigidBody.velocity != Vector2.zero);


        //check if player is within sight radius
        var colliders = Physics2D.OverlapCircleAll(transform.position, sightRadius);
        foreach(Collider2D collider in colliders)
        {
            if (collider.tag == "Player")
            {
                movementMode = Mode.Pursuit;
                playerReference = collider.GetComponentInParent<Player>();
                break;
            }
        }
    }

    void Pursuit()
    {
        Vector2 direction = playerReference.transform.position - transform.position;
        Vector2 newVelocity = direction.normalized * properties.speed;
        properties.rigidBody.velocity = Vector2.SmoothDamp(properties.rigidBody.velocity, newVelocity, ref smoothVelocityReference, 0.1f);

        animator.SetFloat("HorizontalMagnitude", properties.rigidBody.velocity.x);
        animator.SetFloat("VerticalMagnitude", properties.rigidBody.velocity.y);
        animator.SetBool("Moving", properties.rigidBody.velocity != Vector2.zero);

        var colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Player")
            {
                movementMode = Mode.Attack;
                break;
            }
        }
    }

    void Attack()
    {
        float damageAmount = 25.0f;

        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("pauseInput"))
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
            foreach (Collider2D collider in colliders)
            {
                if (collider.tag == "Player")
                {
                    animator.SetTrigger("Attack");
                    playerReference.properties.propertiesManager.InflictPhysicalDamage(damageAmount * properties.physicalAttackMultiplier);
                    playerReference.animator.SetTrigger("PlayerHurt");
                }
            }

            movementMode = Mode.Pursuit;
        }

        properties.rigidBody.velocity = Vector2.zero;

    }


    // Update is called once per frame
    void Update()
    {
        if (properties.health <= 0)
        {
            movementMode = Mode.Disabled;
        }   
    }

    void FixedUpdate()
    {
        switch (movementMode)
        {
            case Mode.Sentry:
                Patrol();
                break;
            case Mode.Pursuit:
                Pursuit();
                break;
            case Mode.Attack:
                Attack();
                break;
            case Mode.Disabled:
                properties.rigidBody.velocity = Vector2.zero;
                if (!animator.GetBool("Disabled"))
                {
                    animator.SetTrigger("Disable");
                    animator.SetBool("Disabled", true);
                    gameObject.GetComponent<Collider2D>().enabled = false;
                }
                break;
            case Mode.Dead:
                break;
        }
    }
}
