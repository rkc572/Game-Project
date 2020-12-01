using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageMovementController : MovementController
{

    public enum MovementMode
    {
        TailClosestSkeleton,
        ReviveDisabledSkeleton
    }

    public Mage mage;

    public MovementMode movementMode = MovementMode.TailClosestSkeleton;


    Vector2 smoothVelocityReference = Vector2.zero;

    public override void StopMoving()
    {
        mage.inputController.detectMovementInput = false;
        mage.rigidBody.velocity = Vector2.zero;
    }

    Skeleton FindSkeletonToTail()
    {
        Skeleton closestSkeleton = null;
        var skeletons = Resources.FindObjectsOfTypeAll(typeof(Skeleton));
        var mageDistance = transform.parent.position;
        float closestDistance = -1.0f;
        foreach (Skeleton skeleton in skeletons)
        {
            if (skeleton.isActiveAndEnabled)
            {

                if (((SkeletonMovementController)skeleton.movementController).movementMode == SkeletonMovementController.MovementMode.Disabled)
                {
                    closestSkeleton = skeleton;
                    return closestSkeleton;
                }


                var distanceFromMage = (skeleton.transform.parent.position - mageDistance).magnitude;

                if (distanceFromMage < closestDistance || closestDistance == -1.0f)
                {
                    closestDistance = distanceFromMage;
                    closestSkeleton = skeleton;
                }
            }
        }

        return closestSkeleton;
    }


    void TailSkeleton()
    {
        Skeleton closestSkeleton = FindSkeletonToTail();

        var distanceFromSkeleton = (closestSkeleton.transform.parent.position - transform.parent.position).magnitude;

        if (((SkeletonMovementController)closestSkeleton.movementController).movementMode == SkeletonMovementController.MovementMode.Disabled)
        {
            Vector2 skeletonDirection = closestSkeleton.transform.parent.position - transform.parent.position;
            Vector2 newVelocity = skeletonDirection.normalized * mage.speed;
            mage.rigidBody.velocity = Vector2.SmoothDamp(mage.rigidBody.velocity, newVelocity, ref smoothVelocityReference, 0.1f);
            return;
        }

        if (distanceFromSkeleton > 0.9f)
        {
            Vector2 skeletonDirection = closestSkeleton.transform.parent.position - transform.parent.position;

            if (((SkeletonMovementController)closestSkeleton.movementController).movementMode != SkeletonMovementController.MovementMode.Disabled)
            {
                skeletonDirection += new Vector2(Random.Range(-1, 1) * Random.value, Random.Range(-1, 1) * Random.value);
            }

            Vector2 newVelocity = skeletonDirection.normalized * mage.speed;
            mage.rigidBody.velocity = Vector2.SmoothDamp(mage.rigidBody.velocity, newVelocity, ref smoothVelocityReference, 0.1f);
        }
        else
        {
            //mage.animator.SetFloat("HorizontalMagnitude", 0.0f);
            //mage.animator.SetFloat("VerticalMagnitude", -1.0f);
            var rotation = transform.parent.rotation;
            mage.rigidBody.velocity = Vector2.SmoothDamp(mage.rigidBody.velocity, Vector2.zero, ref smoothVelocityReference, 0.5f);
            transform.parent.RotateAround(closestSkeleton.transform.parent.position, new Vector3(0.0f, 0.0f, 1.0f), Time.deltaTime * 50.0f);
            transform.parent.rotation = rotation;
        }
    }


    public void Move()
    {
        switch (movementMode)
        {
            case MovementMode.TailClosestSkeleton:
                TailSkeleton();
                break;
        }
    }

}
