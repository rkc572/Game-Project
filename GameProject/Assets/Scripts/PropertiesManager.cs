using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class PropertiesManager : MonoBehaviour
{
    public Mob mob;

    public void ApplyAttachedEffects()
    {
        // Run ApplyEffect for each EffectState in Mob effectStates
        mob.effectStates.ForEach(effectState => effectState.ApplyEffect());
        // Remove EffectStates from mob when they are complete
        mob.effectStates.RemoveAll(effectState => effectState.complete);
    }

    public void ToggleEffectState(EffectState effectState)
    {
        // Add EffectState only if not already in Mob effectStates
        if (!mob.effectStates.Any(mobEffectState => mobEffectState.GetType() == effectState.GetType()))
        {
            mob.effectStates.Add(effectState);
        }
    }

    public void ModifyHealthByAmount(float amount)
    {
        // Limit health to max mob health and minimum 0
        mob.health = Mathf.Clamp(mob.health + amount, 0, mob.MAX_HEALTH);
    }

    public void ModifyManaByAmount(float amount)
    {
        // Limit mana to max mob mana and minimum 0
        mob.mana = Mathf.Clamp(mob.mana + amount, 0, mob.MAX_MANA);
    }

    public void ModifySpeedByAmount(float amount)
    {
        // Limit health to max mob health and minimum 0
        mob.speed = Mathf.Clamp(mob.speed + amount, 0, mob.MAX_SPEED);
    }

    // Use InflictDamage* functions to apply damage to mob including their respective damage multiplier 
    public void InflictDamage(float damage)
    {   
        mob.health = Mathf.Max(mob.health - damage * mob.damageTakenMultiplier, 0);
    }

    public void InflictPhysicalDamage(float damage)
    {
        mob.health = Mathf.Max(mob.health - damage * mob.physicalDamageTakenMultiplier * mob.damageTakenMultiplier, 0);
    }

    public void InflictElementalDamage(float damage)
    {
        mob.health = Mathf.Max(mob.health - damage * mob.elementalDamageTakenMultiplier * mob.damageTakenMultiplier, 0);
    }

    // Use Set* functions whenever you have to bypass any limits and additional computation
    public void SetMobHealth(float newHealth)
    {
        mob.health = newHealth;
    }

    public void SetMobMana(float newMana)
    {
        mob.mana = newMana;
    }

    public void SetMobSpeed(float newSpeed)
    {
        mob.speed = newSpeed;
    }

    public void SetPhysicalAttackMultiplier(float newPhysicalAttackMultiplier)
    {
        mob.physicalAttackMultiplier = newPhysicalAttackMultiplier;
    }

    public void SetElementalAttackMultiplier(float newElementalAttackMultiplier)
    {
        mob.elementalAttackMultiplier = newElementalAttackMultiplier;
    }

    public void SetPhysicalDamageTakenMultiplier(float newPhysicalDamageTakenMultiplier)
    {
        mob.physicalDamageTakenMultiplier = newPhysicalDamageTakenMultiplier;
    }

    public void SetElementalDamageTakenMultiplier(float newElementalDamageTakenMultiplier)
    {
        mob.elementalDamageTakenMultiplier = newElementalDamageTakenMultiplier;
    }

    private void Update()
    {
        ApplyAttachedEffects();
    }

}
