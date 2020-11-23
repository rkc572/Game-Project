using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Mage : Enemy
{
    public SortingGroup sortingGroup;

    Skeleton closestSkeleton = null;
    Vector2 smoothVelocityReference = Vector2.zero;
    bool dead = false;

    public GameObject goldCoinPrefab;
    public GameObject goldBarPrefab;
    public GameObject goldStackPrefab;

    public GameObject healthDropPrefab;
    public GameObject manaDropPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Attack());
    }


    void FindSkeletonToTail()
    {
        var skeletons = Resources.FindObjectsOfTypeAll(typeof(Skeleton));
        var mageDistance = transform.parent.position;
        float closestDistance = -1.0f;
        foreach (Skeleton skeleton in skeletons)
        {
            if (skeleton.isActiveAndEnabled)
            {

                if (skeleton.movementMode == Skeleton.MovementMode.Disabled)
                {
                    closestSkeleton = skeleton;
                    return;
                }


                var distanceFromMage = (skeleton.transform.parent.position - mageDistance).magnitude;

                if (distanceFromMage < closestDistance || closestDistance == -1.0f)
                {
                    closestDistance = distanceFromMage;
                    closestSkeleton = skeleton;
                }
            }
        }
    }

    void TailSkeleton()
    {
        var distanceFromSkeleton = (closestSkeleton.transform.parent.position - transform.parent.position).magnitude;

        if (distanceFromSkeleton > 1.15f)
        {
            Vector2 skeletonDirection = closestSkeleton.transform.parent.position - transform.parent.position;

            if (closestSkeleton.movementMode != Skeleton.MovementMode.Disabled)
            {
                skeletonDirection += new Vector2(Random.Range(-1, 1) * Random.value, Random.Range(-1, 1) * Random.value);
            }

            Vector2 newVelocity = skeletonDirection.normalized * speed;
            rigidBody.velocity = Vector2.SmoothDamp(rigidBody.velocity, newVelocity, ref smoothVelocityReference, 0.1f);
        }
        else
        {
            animator.SetFloat("HorizontalMagnitude", 0.0f);
            animator.SetFloat("VerticalMagnitude", -1.0f);
            var rotation = transform.parent.rotation;
            rigidBody.velocity = Vector2.SmoothDamp(rigidBody.velocity, Vector2.zero, ref smoothVelocityReference, 0.5f);
            transform.parent.RotateAround(closestSkeleton.transform.parent.position, new Vector3(0.0f, 0.0f, 1.0f), Time.deltaTime * 50.0f);
            transform.parent.rotation = rotation;
        }
    }

    IEnumerator Attack()
    {
        while (health > 0) {
            yield return new WaitForSeconds(8.0f);
            if (!dead)
            {
                animator.SetTrigger("Attack");
            }
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

        if (closestSkeleton.movementMode != Skeleton.MovementMode.Disabled)
        {
            animator.SetFloat("HorizontalMagnitude", scaledDirection.x);
            animator.SetFloat("VerticalMagnitude", scaledDirection.y);
        }
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

        List<GameObject> drops = new List<GameObject> { goldCoinPrefab, goldBarPrefab, goldStackPrefab, healthDropPrefab, manaDropPrefab };
        // Drop random drop
        Instantiate(drops[Random.Range(0, drops.Count)], transform.position, Quaternion.identity);

        Destroy(transform.parent.gameObject, 1.0f);
    }

    public void CircleAttack()
    {
        var circleAttackPrefab = (GameObject)Resources.Load("prefabs/MageCircleAttack", typeof(GameObject));
        GameObject.Instantiate(circleAttackPrefab, transform.parent.position - new Vector3(0.0f, 0.25f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            FindSkeletonToTail();
            TailSkeleton();
            UpdateWalkingAnimatorParameters();
        }
        else if(!dead)
        {
            dead = true;
            StartCoroutine(Die());
        }
    }
}
