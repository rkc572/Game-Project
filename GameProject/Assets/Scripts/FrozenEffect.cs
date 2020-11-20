using UnityEngine;
//This effect freezes you and increases physical damage taken
public class FrozenEffect : EffectState
{
    float effectDuration;
    float previousSpeed, previousPhysicalDamageTakenMultiplier;
    float frozenSpeed = 0.0f;
    float newPhysicalDamageTakenMultiplier;

    bool effectApplied = false;

    bool particlesActive = false;
    GameObject frozenParticles;

    public FrozenEffect(PropertiesManager propertiesManager, float effectDuration, float newPhysicalDamageTakenMultiplier) : base(propertiesManager)
    {
        this.effectDuration = effectDuration;
        this.newPhysicalDamageTakenMultiplier = newPhysicalDamageTakenMultiplier;
        previousSpeed = propertiesManager.mob.speed;
        previousPhysicalDamageTakenMultiplier = propertiesManager.mob.physicalDamageTakenMultiplier;
    }

    protected override void Effect()
    {
        propertiesManager.SetMobSpeed(frozenSpeed);
        propertiesManager.SetPhysicalDamageTakenMultiplier(newPhysicalDamageTakenMultiplier);
        Debug.Log("I am frozen and take more damage!");
    }

    public override void ApplyEffect()
    {
        //apply effect if not already
        if (!effectApplied)
        {
            Effect();
            effectApplied = true;
        }
        if (!particlesActive)
        {
            var frozenParticlesPrefab = (GameObject)Resources.Load("prefabs/MobIsFrozen", typeof(GameObject));
            frozenParticles = GameObject.Instantiate(frozenParticlesPrefab, Vector3.zero, Quaternion.identity);
            frozenParticles.transform.position = Vector3.zero;
            frozenParticles.transform.SetParent(propertiesManager.mob.transform, false);
            particlesActive = true;
        }
        //after effect duration finishes, reset
        if (Time.time > effectInitializedTime + effectDuration || complete)
        {
            complete = true;
            GameObject.Destroy(frozenParticles);
            propertiesManager.SetMobSpeed(previousSpeed);
            propertiesManager.SetPhysicalDamageTakenMultiplier(previousPhysicalDamageTakenMultiplier);
            Debug.Log("I am no longer frozen");
            effectApplied = false;
        }
    }




}
