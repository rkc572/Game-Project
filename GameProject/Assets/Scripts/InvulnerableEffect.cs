using UnityEngine;

public class InvulnerableEffect : EffectState
{
    float effectDuration;
    float previousPhysicalDamageTakenMultipler, previousElementalDamageTakenMultipler;
    float newPhysicalDamageTakenMultipler, newElementalDamageTakenMultiplier = 0.0f;
    //enemy colliders ignored/attack input disabled

    bool effectApplied = false;

    public InvulnerableEffect(PropertiesManager propertiesManager, float effectDuration) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        previousPhysicalDamageTakenMultipler = propertiesManager.mob.PhysicalDamageTakenMultiplier;
        previousElementalDamageTakenMultipler = propertiesManager.mob.ElementalDamageTakenMultiplier;
    }
    
}
