using UnityEngine;

public class VulnerableEffect : EffectState
{
    float effectDuration;
    float previousDamageTakenMultiplier;
    //Damage taken multiplier increases by 50%
    float newDamageTakenMultiplier = 1.5f;

    bool effectApplied = false;

    public VulnerableEffect(PropertiesManager propertiesManager, float effectDuration) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        previousDamageTakenMultiplier = propertiesManager.mob.physicalDamageTakenMultiplier;
    }

    protected override void Effect()
    {
        propertiesManager.SetPhysicalDamageTakenMultiplier(newDamageTakenMultiplier);
        Debug.Log("I am vulnerable!");
    }

    public override void ApplyEffect()
    {
        //apply effect if not already
        if (!effectApplied)
        {
            Effect();
            effectApplied = true;
        }
        //after effect duration finishes, reset
        if(Time.time > effectInitializedTime + effectDuration || complete)
        {
            complete = true;
            propertiesManager.SetPhysicalDamageTakenMultiplier(previousDamageTakenMultiplier);
            Debug.Log("I am no longer vulnerable");
            effectApplied = false;
        }
    }
}
