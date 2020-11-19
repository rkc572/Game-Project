using UnityEngine;
//This effect decreases speed
public class SlowedEffect : EffectState
{
    float effectDuration;

    float previousSpeed;
    float newSpeed;

    bool effectApplied = false;

    public SlowedEffect(PropertiesManager propertiesManager, float effectDuration, float newSpeed) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        this.newSpeed = newSpeed;
        previousSpeed = propertiesManager.mob.speed;
    }
    
    protected override void Effect()
    {
        Debug.Log($"I'm slowed down! {Time.time}");
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
        if(Time.time > effectInitializedTime + effectDuration || complete)
        {
            complete = true;
            propertiesManager.SetMobSpeed(previousSpeed);
            Debug.Log("I'm no longer slowed!");
        }
    }
}
