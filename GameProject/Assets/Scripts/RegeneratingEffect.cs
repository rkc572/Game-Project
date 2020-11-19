using UnityEngine;
//This effect allows regeneration
public class RegeneratingEffect : EffectState
{
    float effectDuration;
    float lastInflictionTime = 0.0f;
    float actionInterval;
    float healthRegenAmount;

    public RegeneratingEffect(PropertiesManager propertiesManager, float actionInterval, float effectDuration, float healthRegenAmount) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        this.actionInterval = actionInterval;
        this.healthRegenAmount = healthRegenAmount;
    }

    protected override void Effect()
    {
        Debug.Log($"im healing at {Time.time}");
        propertiesManager.ModifyHealthByAmount(healthRegenAmount);
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
            Debug.Log($"im done regenerating at {Time.time}");
        }
    }
}