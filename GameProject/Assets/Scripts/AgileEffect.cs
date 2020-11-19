using UnityEngine;

//This effect enhances speed
public class AgileEffect : EffectState
{
    //How long effect lasts
    float effectDuration;

    //Hold previous speed value
    float previousSpeed;

    //Get new speed value
    float newSpeed;

    bool effectApplied = false;

    public AgileEffect(PropertiesManager propertiesManager, float effectDuration, float newSpeed) : base(propertiesManager)
    {
        //Get duration/new speed
        this.effectDuration = effectDuration;
        this.newSpeed = newSpeed;
        previousSpeed = propertiesManager.mob.speed;
    }

    protected override void Effect()
    {
        Debug.Log($"I'm faster! {Time.time}");
        //Set new speed
        propertiesManager.SetMobSpeed(newSpeed);
    }

    //Apply
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
