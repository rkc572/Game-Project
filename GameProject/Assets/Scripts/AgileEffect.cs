using UnityEngine;

public class AgileEffect : EffectState
{
    float effectDuration;

    float previousSpeed;
    float newSpeed = 1.5f;

    bool effectApplied = false;

    public AgileEffect(PropertiesManager propertiesManager, float effectDuration) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        previousSpeed = propertiesManager.mob.speed;
    }

    protected override void Effect()
    {
        Debug.Log($"I'm faster! {Time.time}");
        propertiesManager.SetMobSpeed(newSpeed);
    }

    public override void ApplyEffect()
    {
        // Apply Effect once
        if (!effectApplied)
        {
            Effect();
            effectApplied = true;
        }

        // Reset after effect duration has run out
        if (Time.time > effectInitializedTime + effectDuration || complete)
        {
            complete = true;
            propertiesManager.SetMobSpeed(previousSpeed);
            Debug.Log("I'm no longer fast!");
        }
    }
}
