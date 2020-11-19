using UnityEngine;
//This effect stuns/freezes you
public class StunnedEffect : EffectState
{
    float effectDuration;
    float previousSpeed;
    //Stunned Effect - frozen cant move
    float stunnedSpeed = 0.0f;
    bool effectApplied = false;


    public StunnedEffect(PropertiesManager propertiesManager, float effectDuration) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        previousSpeed = propertiesManager.mob.speed;
    }

    protected override void Effect()
    {
        Debug.Log("I am stunned - can't move");
        propertiesManager.SetMobSpeed(stunnedSpeed);
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
            Debug.Log("I am no longer stunned - I can move");
            propertiesManager.SetMobSpeed(previousSpeed);
            effectApplied = false;
        }
    }
}
