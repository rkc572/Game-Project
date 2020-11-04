using UnityEngine;

public class RegeneratingEffect : EffectState
{
    float effectDuration;

    public RegeneratingEffect(PropertiesManager propertiesManager, float effectDuration) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
    }

    protected override void Effect()
    {

    }
    public override void ApplyEffect()
    {

    }
}
