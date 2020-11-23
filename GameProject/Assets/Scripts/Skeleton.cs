using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Skeleton : Enemy
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

    public SortingGroup sortingGroup;

    public bool droppedItem = false;
    public GameObject goldCoinPrefab;
    public GameObject goldBarPrefab;
    public GameObject goldStackPrefab;

    public GameObject healthDropPrefab;
    public GameObject manaDropPrefab;

    public float sightRadius = 0.4f;
    public float attackRadius = 0.15f;

    public MovementMode movementMode = MovementMode.Patrol;
    public PatrolMode patrolMode = PatrolMode.HorizontallyRight;

    Player playerReference;


    // Patrol Mode variables;
    public float minHorizontalMovement = 1.0f;
    public float maxHorizontalMovement = 1.0f;
    public float minVerticalMovement = 1.0f;
    public float maxVerticalMovement = 1.0f;
    Vector3 startingPosition;


    Vector2 smoothVelocityReference = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        // Record initial starting position
        startingPosition = transform.position;
    }

    void UpdateWalkingAnimatorParameters()
    {
        animator.SetFloat("HorizontalMagnitude", rigidBody.velocity.x);
        animator.SetFloat("VerticalMagnitude", rigidBody.velocity.y);
        animator.SetBool("Moving", rigidBody.velocity.y != 0.0f || rigidBody.velocity.x != 0);
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

        rigidBody.velocity = direction * speed;
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
        if (playerReference == null)
        {
            movementMode = MovementMode.Patrol;
            return;
        }

        Vector3 playerOffset = new Vector3(0.0f, 0.1f, 0.0f);

        Vector2 playerDirection = (playerReference.transform.position + playerOffset) - transform.position;
        Vector2 newVelocity = playerDirection.normalized * speed;
        rigidBody.velocity = Vector2.SmoothDamp(rigidBody.velocity, newVelocity, ref smoothVelocityReference, 0.1f);

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
        if (playerReference == null)
        {
            movementMode = MovementMode.Patrol;
            return;
        }

        float damageAmount = 25.0f;

        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("pauseInput"))
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position - new Vector3(0.0f, 0.05f, 0.0f), attackRadius);
            foreach (Collider2D collider in colliders)
            {
                if (collider.tag == "Player")
                {
                    animator.SetTrigger("Attack");
                    playerReference.InflictPhysicalDamage(damageAmount * physicalAttackMultiplier * attackMultiplier);
                    playerReference.animator.SetTrigger("PlayerHurt");
                    playerReference.ToggleEffectState(new RepulsedEffect(playerReference, 0.1f, new Vector2(animator.GetFloat("HorizontalMagnitude"), animator.GetFloat("VerticalMagnitude")), 2.0f));
                }
            }

            movementMode = MovementMode.Pursuit;
        }

        rigidBody.velocity = Vector2.zero;

    }

    void Disable()
    {
        rigidBody.velocity = Vector2.zero;
        if (!animator.GetBool("Disabled"))
        {
            animator.SetTrigger("Disable");
            animator.SetBool("Disabled", true);
            gameObject.GetComponent<Collider2D>().isTrigger = true;
            sortingGroup.sortingOrder = -1;
        }

        if (!droppedItem)
        {
            List<GameObject> drops = new List<GameObject> {goldCoinPrefab, goldBarPrefab, goldStackPrefab, healthDropPrefab, manaDropPrefab};
            // Drop random drop
            Instantiate(drops[Random.Range(0, drops.Count)], transform.position, Quaternion.identity);
            droppedItem = true;
        }
    }

    public void Revive()
    {
        animator.SetBool("Disabled", false);
        sortingGroup.sortingOrder = 0;
        movementMode = MovementMode.Pursuit;
        gameObject.GetComponent<Collider2D>().isTrigger = false;
        health = MAX_HEALTH;
    }


    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
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
                Disable();
                break;
            case MovementMode.Dead:
                break;
        }
    }
}
