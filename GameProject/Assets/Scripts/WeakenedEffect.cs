using UnityEngine;

public class WeakenedEffect : EffectState
{
    
    float effectDuration;

    float previousDamageMultiplier;
    //Halves damage
    float newDamageMultiplier = 0.5f;

    bool effectApplied = false;

    public WeakenedEffect(PropertiesManager propertiesManager, float effectDuration) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        previousDamageMultiplier = propertiesManager.mob.physicalAttackMultiplier;
    }

    protected override void Effect()
    {
        Debug.Log("I'm weakened!");
        propertiesManager.SetPhysicalAttackMultiplier(newDamageMultiplier);
    }

    public override void ApplyEffect()
    {
        //apply effect if not already
        if (!effectApplied)
        {
            Effect();
            effectApplied = true;
        }

        //After effect duration finishes, reset
        if(Time.time > effectInitializedTime + effectDuration || complete)
        {
            complete = true;
            propertiesManager.SetPhysicalAttackMultiplier(previousDamageMultiplier);
            Debug.Log("I'm not weakened!");
            effectApplied = false;
        }
    }

   
}
