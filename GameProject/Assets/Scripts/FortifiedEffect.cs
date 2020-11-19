using UnityEngine;
//This effect decreases physical damage taken multipler - Tank Effect
public class FortifiedEffect : EffectState
{
    float effectDuration;
    float previousPhysicalDamageTakenMultiplier;
    float newPhysicalDamageTakenMultiplier;

    bool effectApplied = false;

    public FortifiedEffect(PropertiesManager propertiesManager, float effectDuration, float newPhysicalDamageTakenMultiplier) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        this.newPhysicalDamageTakenMultiplier = newPhysicalDamageTakenMultiplier;
        previousPhysicalDamageTakenMultiplier = propertiesManager.mob.physicalDamageTakenMultiplier;
    }

    protected override void Effect()
    {
        propertiesManager.SetPhysicalDamageTakenMultiplier(newPhysicalDamageTakenMultiplier);
        Debug.Log("I am fortified - less phys.dmg taken");
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
            propertiesManager.SetPhysicalDamageTakenMultiplier(previousPhysicalDamageTakenMultiplier);
            Debug.Log("I am no longer fortified");
            effectApplied = false;
        }
    }
    
}
