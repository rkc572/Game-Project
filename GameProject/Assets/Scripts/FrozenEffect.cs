using UnityEngine;

public class FrozenEffect : EffectState
{
    float effectDuration;
    float previousSpeed, previousPhysicalDamageTakenMultiplier;
    float frozenSpeed = 0.0f;
    float newPhysicalDamageTakenMultiplier = 1.5f;

    bool effectApplied = false;

    public FrozenEffect(PropertiesManager propertiesManager, float effectDuration) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        previousSpeed = propertiesManager.mob.speed;
        previousPhysicalDamageTakenMultiplier = propertiesManager.mob.physicalDamageTakenMultiplier;
    }

    protected override void Effect()
    {
        propertiesManager.SetMobSpeed(frozenSpeed);
        propertiesManager.SetPhysicalDamageTakenMultiplier(newPhysicalDamageTakenMultiplier);
        Debug.Log("I am frozen and take more damage!");
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
        if (Time.time > effectInitializedTime + effectDuration || complete)
        {
            complete = true;
            propertiesManager.SetMobSpeed(previousSpeed);
            propertiesManager.SetPhysicalDamageTakenMultiplier(previousPhysicalDamageTakenMultiplier);
            Debug.Log("I am no longer frozen");
            effectApplied = false;
        }
    }




}
