using UnityEngine;
//This effect increases Physical Attack Multiplier
public class StrengthenedEffect : EffectState
{
    float effectDuration;
    float previousPhysicalAttackMultiplier;
    float newPhysicalAttackMultiplier;

    bool effectApplied = false;

    public StrengthenedEffect(PropertiesManager propertiesManager, float effectDuration, float newPhysicalAttackMultiplier) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        this.newPhysicalAttackMultiplier = newPhysicalAttackMultiplier;
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
