using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public enum LineOfSight
{
    NONE,
    UP,
    LEFT,
    DOWN,
    RIGHT
}

public class Mob : MonoBehaviour
{
    public Animator animator;
    public InputController inputController;
    public MovementController movementController;
    public Rigidbody2D rigidBody;

    [Header("Stats")]
    public float MAX_HEALTH = 1000;
    public float MAX_MANA = 1000;
    public float MAX_SPEED = 1.0f;

    public float health = 1000;
    public float mana = 1000;
    public float speed = 1.0f;

    public float attackMultiplier = 1f;
    public float physicalAttackMultiplier = 1f;
    public float elementalAttackMultiplier = 1f;

    public float damageTakenMultiplier = 1f;
    public float physicalDamageTakenMultiplier = 1f;
    public float elementalDamageTakenMultiplier = 1f;

    public List<EffectState> effectStates = new List<EffectState>();

    public LineOfSight GetLineOfSight()
    {

        float verticalMagnitude = animator.GetFloat("VerticalMagnitude");
        float horizontalMagnitude = animator.GetFloat("HorizontalMagnitude");

        LineOfSight direction = LineOfSight.NONE;

        if (horizontalMagnitude == 0 && verticalMagnitude == -1)
        {
            direction = LineOfSight.DOWN;
        }
        else if (horizontalMagnitude == 0 && verticalMagnitude == 1)
        {
            direction = LineOfSight.UP;
        }
        else if(horizontalMagnitude == -1)
        {
            direction = LineOfSight.LEFT;
        }
        else if (horizontalMagnitude == 1)
        {
            direction = LineOfSight.RIGHT;
        }
        
        return direction;
    }


    public virtual IEnumerator KnockBack(Vector2 attackDirection, float force)
    {
        movementController.StopMoving();

        animator.SetFloat("HorizontalMagnitude", -attackDirection.x);
        animator.SetFloat("VerticalMagnitude", -attackDirection.y);

        Debug.Log(attackDirection);
        rigidBody.velocity = attackDirection * force;

        yield return new WaitForSeconds(0.05f);
        rigidBody.velocity = Vector2.zero;
    }



    public void ApplyAttachedEffects()
    {
        // Run ApplyEffect for each EffectState in Mob effectStates
        effectStates.ForEach(effectState => effectState.ApplyEffect());
        // Remove EffectStates from mob when they are complete
        effectStates.RemoveAll(effectState => effectState.complete);
    }

    public void ToggleEffectState(EffectState effectState)
    {

        // Add EffectState only if not already in Mob effectStates
        if (!effectStates.Any(mobEffectState => mobEffectState.GetType() == effectState.GetType()))
        {
            effectStates.Add(effectState);
        }
        // Else reset effectStateTime
        else
        {
            // TODO
            // effectState.ResetEffectDuration();
        }
    }

    public void ModifyHealthByAmount(float amount)
    {
        // Limit health to max mob health and minimum 0
        health = Mathf.Clamp(health + amount, 0, MAX_HEALTH);
    }

    public void ModifyManaByAmount(float amount)
    {
        // Limit mana to max mob mana and minimum 0
        mana = Mathf.Clamp(mana + amount, 0, MAX_MANA);
    }

    public void ModifySpeedByAmount(float amount)
    {
        // Limit health to max mob health and minimum 0
        speed = Mathf.Clamp(speed + amount, 0, MAX_SPEED);
    }

    // Use InflictDamage* functions to apply damage to mob including their respective damage multiplier 
    public void InflictDamage(float damage)
    {
        health = Mathf.Max(health - damage * damageTakenMultiplier, 0);
    }

    public void InflictPhysicalDamage(float damage)
    {
        health = Mathf.Max(health - damage * physicalDamageTakenMultiplier * damageTakenMultiplier, 0);
    }

    public void InflictElementalDamage(float damage)
    {
        health = Mathf.Max(health - damage * elementalDamageTakenMultiplier * damageTakenMultiplier, 0);
    }

    // Use Set* functions whenever you have to bypass any limits and additional computation
    public void SetMobHealth(float newHealth)
    {
        health = newHealth;
    }

    public void SetMobMana(float newMana)
    {
        mana = newMana;
    }

    public void SetMobSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetPhysicalAttackMultiplier(float newPhysicalAttackMultiplier)
    {
        physicalAttackMultiplier = newPhysicalAttackMultiplier;
    }

    public void SetElementalAttackMultiplier(float newElementalAttackMultiplier)
    {
        elementalAttackMultiplier = newElementalAttackMultiplier;
    }

    public void SetPhysicalDamageTakenMultiplier(float newPhysicalDamageTakenMultiplier)
    {
        physicalDamageTakenMultiplier = newPhysicalDamageTakenMultiplier;
    }

    public void SetElementalDamageTakenMultiplier(float newElementalDamageTakenMultiplier)
    {
        elementalDamageTakenMultiplier = newElementalDamageTakenMultiplier;
    }

    protected virtual void Update()
    {
        ApplyAttachedEffects();
    }

}
