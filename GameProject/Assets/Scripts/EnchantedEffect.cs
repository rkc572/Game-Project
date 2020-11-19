using UnityEngine;
//This effect increases Elemental Attack Multiplier
public class EnchantedEffect : EffectState
{
    float effectDuration;
    float previousElementalAttackMultiplier;
    float newElementalAttackMultiplier;

    bool effectApplied = false;

    public EnchantedEffect(PropertiesManager propertiesManager, float effectDuration, float newElementalAttackMultiplier) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        this.newElementalAttackMultiplier = newElementalAttackMultiplier;
        previousElementalAttackMultiplier = propertiesManager.mob.elementalAttackMultiplier;
    }

    protected override void Effect()
    {
        Debug.Log("I am enchanted!");
        propertiesManager.SetElementalAttackMultiplier(newElementalAttackMultiplier);
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
            propertiesManager.SetElementalAttackMultiplier(previousElementalAttackMultiplier);
            Debug.Log("I am no longer enchanted");
            effectApplied = false;
        }
    }
   
}
