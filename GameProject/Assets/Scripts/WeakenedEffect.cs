using UnityEngine;

//This effect lowers Physical Attack Multiplier
public class WeakenedEffect : EffectState
{
    
    float effectDuration;

    float previousDamageMultiplier;
    //Halves damage
    float newDamageMultiplier;

    bool effectApplied = false;

    public WeakenedEffect(PropertiesManager propertiesManager, float effectDuration, float newDamageMultiplier) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        this.newDamageMultiplier = newDamageMultiplier;
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
