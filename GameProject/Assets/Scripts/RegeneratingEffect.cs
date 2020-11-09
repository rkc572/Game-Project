using UnityEngine;

//Incomplete Effect*******
public class RegeneratingEffect : EffectState
{
    //Need to set conditional in Mob/Player class; ex. if player.curHealth < player.max_HP, etc
    float effectDuration;
    float regenInterval;

    float regenAmount = 10.0f;

    bool effectApplied = false;

    public RegeneratingEffect(PropertiesManager propertiesManager, float effectDuration, float regenInterval) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        this.regenInterval = regenInterval;

    }

    protected override void Effect()
    {
        Debug.Log("I am regening");
        propertiesManager.ModifyHealthByAmount(regenAmount);

    }
    public override void ApplyEffect()
    {
        //apply effect if not already
        if (!effectApplied)
        {
            Effect();
            regenInterval -= Time.deltaTime;
            effectApplied = true;
        }
        //after effect duration finishes, reset
        if (Time.time > effectInitializedTime + effectDuration && regenInterval == 0 || complete && regenInterval == 0)
        {
            complete = true;
            Debug.Log("I am no longer regening");
            effectApplied = false;
        }

    }
}
