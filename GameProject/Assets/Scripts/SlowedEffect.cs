using UnityEngine;

public class SlowedEffect : EffectState
{
    float effectDuration;
    float lastInflictionTime = 0.0f;
    float actionInterval;

    public SlowedEffect(PropertiesManager propertiesManager, float actionInterval, float effectDuration) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        this.actionInterval = actionInterval;
    }
    
    protected override void Effect()
    {
        Debug.Log($"I'm slowed down! {Time.time}");
        propertiesManager.SetMobSpeed(0.5f);

    }
    
    public override void ApplyEffect()
    {
        //Same logic as Burning Effect
        if(lastInflictionTime == 0.0f)
        {
            lastInflictionTime = Time.time;
        }
        if(Time.time < effectInitializedTime + effectDuration && !complete)
        {
            if(Time.time - lastInflictionTime >= actionInterval)
            {
                Effect();
                lastInflictionTime = Time.time;
            }
        }
        else
        {
            complete = true;
            Debug.Log("I'm no longer slowed!");
        }
    }
}
