using UnityEngine;

public class StrengthenedEffect : EffectState
{
    float effectDuration;
    float previousPhysicalAttackMultiplier;
    float newPhysicalAttackMultiplier = 1.5f;

    bool effectApplied = false;

    public StrengthenedEffect(PropertiesManager propertiesManager, float effectDuration) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        previousPhysicalAttackMultiplier = propertiesManager.mob.physicalAttackMultiplier;
    }

    protected override void Effect()
    {
        Debug.Log("I am strengthened");
        propertiesManager.SetPhysicalAttackMultiplier(newPhysicalAttackMultiplier);
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
            propertiesManager.SetPhysicalAttackMultiplier(previousPhysicalAttackMultiplier);
            Debug.Log("I am no longer strengthened");
            effectApplied = false;
        }
    }
}
