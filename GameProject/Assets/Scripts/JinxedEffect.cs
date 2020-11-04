using UnityEngine;

public class JinxedEffect : EffectState
{
    float effectDuration;
    float previousElementalAttackMultiplier;
    float newElementalAttackMultipler = 0.5f;

    bool effectApplied = false;

    public JinxedEffect(PropertiesManager propertiesManager, float effectDuration) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        previousElementalAttackMultiplier = propertiesManager.mob.elementalAttackMultiplier;
    }

    protected override void Effect()
    {
        propertiesManager.SetElementalAttackMultiplier(newElementalAttackMultipler);
        Debug.Log("I am jinxed - Elemental Attack Multiplier decreased");

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
            Debug.Log("I am no longer jinxed");
            effectApplied = false;
        }
    }

}
