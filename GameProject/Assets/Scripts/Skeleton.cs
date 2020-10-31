using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public enum MovementMode
    {
        Patrol,
        Pursuit,
        Attack,
        Disabled,
        Dead
    };

    public enum PatrolMode
    {
        HorizontallyRight,
        HorizontallyLeft,
        VerticallyUp,
        VerticallyDown,
    };

    public Mob properties;
    public Animator animator;
    public float goldDropOnDeath = 1.0f;
    public float sightRadius = 0.4f;
    public float attackRadius = 0.15f;

    public MovementMode movementMode = MovementMode.Patrol;
    public PatrolMode patrolMode = PatrolMode.HorizontallyRight;

    Player playerReference;


    // Patrol Mode variables;
    float minHorizontalMovement = 1.0f;
    float maxHorizontalMovement = 1.0f;
    float minVerticalMovement = 1.0f;
    float maxVerticalMovement = 1.0f;
    Vector3 startingPosition;


    Vector2 smoothVelocityReference = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        // Assign random range of gold to drop upon death
        goldDropOnDeath = Random.Range(0.0f, 15.0f);
        // Record initial starting position
        startingPosition = transform.position;
    }

    void UpdateWalkingAnimatorParameters()
    {
        animator.SetFloat("HorizontalMagnitude", properties.rigidBody.velocity.x);
        animator.SetFloat("VerticalMagnitude", properties.rigidBody.velocity.y);
        animator.SetBool("Moving", properties.rigidBody.velocity.y != 0.0f || properties.rigidBody.velocity.x != 0);
    }

    void PatrolBoundsUpdate()
    {
        if (patrolMode == PatrolMode.HorizontallyRight && transform.position.x >= startingPosition.x + maxHorizontalMovement)
        {
            patrolMode = PatrolMode.HorizontallyLeft;
        }
        else if (patrolMode == PatrolMode.HorizontallyLeft && transform.position.x <= startingPosition.x - minHorizontalMovement)
        {
            patrolMode = PatrolMode.HorizontallyRight;
        }
        else if (patrolMode == PatrolMode.VerticallyUp && transform.position.y >= startingPosition.y + maxVerticalMovement)
        {
            patrolMode = PatrolMode.VerticallyDown;
        }
        else if (patrolMode == PatrolMode.VerticallyDown && transform.position.y <= startingPosition.y - minVerticalMovement)
        {
            patrolMode = PatrolMode.VerticallyUp;
        }
    }

    void Patrol()
    {
        PatrolBoundsUpdate();

        Vector2 direction;

        switch (patrolMode)
        {
            case PatrolMode.HorizontallyLeft:
                direction = new Vector2(-1.0f, 0.0f);
                break;
            case PatrolMode.HorizontallyRight:
                direction = new Vector2(1.0f, 0.0f);
                break;
            case PatrolMode.VerticallyUp:
                direction = new Vector2(0.0f, 1.0f);
                break;
            case PatrolMode.VerticallyDown:
                direction = new Vector2(0.0f, -1.0f);
                break;
            default:
                direction = new Vector2(0.0f, 0.0f);
                break;
        }

        properties.rigidBody.velocity = direction * properties.speed;
        UpdateWalkingAnimatorParameters();

        //check if player is within sight radius
        var colliders = Physics2D.OverlapCircleAll(transform.position, sightRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Player")
            {
                movementMode = MovementMode.Pursuit;
                playerReference = collider.GetComponentInParent<Player>();
                break;
            }
        }
    }

    void Pursuit()
    {
        Vector2 playerDirection = playerReference.transform.position - transform.position;
        Vector2 newVelocity = playerDirection.normalized * properties.speed;
        properties.rigidBody.velocity = Vector2.SmoothDamp(properties.rigidBody.velocity, newVelocity, ref smoothVelocityReference, 0.1f);

        UpdateWalkingAnimatorParameters();

        var colliders = Physics2D.OverlapCircleAll(transform.position - new Vector3(0.0f, 0.05f, 0.0f), attackRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Player")
            {
                movementMode = MovementMode.Attack;
                break;
            }
        }
    }

    void Attack()
    {
        float damageAmount = 25.0f;

        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("pauseInput"))
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position - new Vector3(0.0f, 0.05f, 0.0f), attackRadius);
            foreach (Collider2D collider in colliders)
            {
                if (collider.tag == "Player")
                {
                    animator.SetTrigger("Attack");
                    playerReference.properties.propertiesManager.InflictPhysicalDamage(damageAmount * properties.physicalAttackMultiplier);
                    playerReference.animator.SetTrigger("PlayerHurt");
                }
            }

            movementMode = MovementMode.Pursuit;
        }

        properties.rigidBody.velocity = Vector2.zero;

    }


    // Update is called once per frame
    void Update()
    {
        if (properties.health <= 0)
        {
            movementMode = MovementMode.Disabled;
        }   
    }

    void FixedUpdate()
    {
        switch (movementMode)
        {
            case MovementMode.Patrol:
                Patrol();
                break;
            case MovementMode.Pursuit:
                Pursuit();
                break;
            case MovementMode.Attack:
                Attack();
                break;
            case MovementMode.Disabled:
                properties.rigidBody.velocity = Vector2.zero;
                if (!animator.GetBool("Disabled"))
                {
                    animator.SetTrigger("Disable");
                    animator.SetBool("Disabled", true);
                    gameObject.GetComponent<Collider2D>().enabled = false;
                }
                break;
            case MovementMode.Dead:
                break;
        }
    }
}
