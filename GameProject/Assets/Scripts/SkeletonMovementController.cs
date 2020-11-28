using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovementController : MovementController
{
    public Skeleton skeleton;

    public enum MovementMode
    {
        Patrol,
        Pursuit,
        Disabled,
    };

    public enum PatrolMode
    {
        HorizontallyRight,
        HorizontallyLeft,
        VerticallyUp,
        VerticallyDown,
    };

    // Patrol Mode variables;
    public float sightRadius = 0.4f;
    public float minHorizontalMovement = 1.0f;
    public float maxHorizontalMovement = 1.0f;
    public float minVerticalMovement = 1.0f;
    public float maxVerticalMovement = 1.0f;
    Vector3 startingPosition;

    public MovementMode movementMode;
    public PatrolMode patrolMode;
    Vector2 smoothVelocityReference = Vector2.zero;


    public override void StopMoving()
    {
        skeleton.inputController.detectMovementInput = false;
        skeleton.rigidBody.velocity = Vector2.zero;
        UpdateWalkingAnimatorParameters();
    }

    void UpdateWalkingAnimatorParameters()
    {
        var player = Player.Instance;

        Vector3 playerOffset = new Vector3(0.0f, 0.1f, 0.0f);
        Vector3 target = (player.transform.position + playerOffset);
        Vector2 targetDirection = target - transform.position;
        Vector2 newVelocity = targetDirection.normalized * skeleton.speed;

        var scaledDirection = newVelocity;

        if (movementMode == MovementMode.Patrol)
        {
            scaledDirection = skeleton.rigidBody.velocity;
        }
        
        if (scaledDirection.y > skeleton.speed / 2)
        {
            scaledDirection.y = 1;
        }
        else if (scaledDirection.y < -1 * (skeleton.speed / 2))
        {
            scaledDirection.y = -1;
        }
        else
        {
            scaledDirection.y = 0;
        }

        if (scaledDirection.x > skeleton.speed / 2)
        {
            scaledDirection.x = 1;
        }
        else if (scaledDirection.x < -1 * (skeleton.speed / 2))
        {
            scaledDirection.x = -1;
        }
        else
        {
            scaledDirection.x = 0;
        }

        skeleton.animator.SetFloat("HorizontalMagnitude", scaledDirection.x);
        skeleton.animator.SetFloat("VerticalMagnitude", scaledDirection.y);
        skeleton.animator.SetBool("Moving", scaledDirection.y != 0.0f || scaledDirection.x != 0);
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

        skeleton.rigidBody.velocity = direction * skeleton.speed;
        UpdateWalkingAnimatorParameters();

        //check if player is within sight radius
        var colliders = Physics2D.OverlapCircleAll(transform.position, sightRadius);
        foreach (Collider2D collider in colliders)
        {
            var player = collider.GetComponentInParent<Player>();

            if (player != null && !player.isEthereal)
            {
                movementMode = MovementMode.Pursuit;
                break;
            }
        }
    }

    void Pursuit() {
        var player = Player.Instance;

        Vector3 playerOffset = new Vector3(0.0f, 0.1f, 0.0f);
        Vector3 target = (player.transform.position + playerOffset);

        if (player.isEthereal)
        {
            movementMode = MovementMode.Patrol;
            return;
            //target = startingPosition;
        }

        Vector2 targetDirection = target - transform.position;
        Vector2 newVelocity = targetDirection.normalized * skeleton.speed;
        skeleton.rigidBody.velocity = Vector2.SmoothDamp(skeleton.rigidBody.velocity, newVelocity, ref smoothVelocityReference, 0.1f);

        UpdateWalkingAnimatorParameters();
    }

    void Disable()
    {
        if (!skeleton.animator.GetBool("Disabled"))
        {
            StopMoving();
            skeleton.animator.SetTrigger("Disable");
            skeleton.animator.SetBool("Disabled", true);
            skeleton.skeletonCollider.isTrigger = true;
            skeleton.sortingGroup.sortingOrder = -1;
            skeleton.gameObject.layer = 14; // DEAD LAYER
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = gameObject.transform.position;
    }

    public void Move()
    {
        switch (movementMode)
        {
            case MovementMode.Patrol:
                Patrol();
                break;
            case MovementMode.Pursuit:
                Pursuit();
                break;
            case MovementMode.Disabled:
                Disable();
                break;
        }
    }

    private void Update()
    {
        if (skeleton.health <= 0)
        {
            movementMode = MovementMode.Disabled;
        }
    }

}
