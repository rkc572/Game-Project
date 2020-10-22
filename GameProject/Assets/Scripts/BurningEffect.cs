using UnityEngine;

public class BurningEffect : EffectState
{
    float effectDuration;
    float lastInflictionTime = 0.0f;
    float actionInterval;

    public BurningEffect(PropertiesManager propertiesManager, float actionInterval, float effectDuration) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        this.actionInterval = actionInterval;
    }

    public override void Effect()
    {
        Debug.Log($"im burning at {Time.time}");
        propertiesManager.InflictDamage(10.0f);
    }

    public override void ApplyEffect()
    {
        // Initialize lastInflictionTime
        if (lastInflictionTime == 0.0f)
        {
            lastInflictionTime = Time.time;
        }

        // Check if EffectState is still active
        if (Time.time < effectInitializedTime + effectDuration && !complete)
        {
            // Check if it is within the interval to apply effect
            if (Time.time - lastInflictionTime >= actionInterval)
            {
                // Apply effect
                Effect();

                // Reset LastInflictionTime
                lastInflictionTime = Time.time;
            }
        }
        else
        {
            // Set complete to true if Effect state has run out and was not marked complete
            complete = true;
            Debug.Log($"im done burning at {Time.time}");
        }
    }
}