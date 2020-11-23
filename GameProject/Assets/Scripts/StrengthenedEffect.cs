using UnityEngine;
//This effect increases Physical Attack Multiplier
public class StrengthenedEffect : EffectState
{
    float effectDuration;
    float previousPhysicalAttackMultiplier;
    float newPhysicalAttackMultiplier;

    bool effectApplied = false;

    bool particlesActive = false;
    GameObject strengthenedParticles;

    public StrengthenedEffect(Mob mob, float effectDuration, float newPhysicalAttackMultiplier) : base(mob)
    {
        this.effectDuration = effectDuration;
        this.newPhysicalAttackMultiplier = newPhysicalAttackMultiplier;
        previousPhysicalAttackMultiplier = mob.physicalAttackMultiplier;
    }

    protected override void Effect()
    {
        Debug.Log("I am strengthened");
        mob.SetPhysicalAttackMultiplier(newPhysicalAttackMultiplier);
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
            var strengthenedParticlesPrefab = (GameObject)Resources.Load("prefabs/MobIsStrengthened", typeof(GameObject));
            strengthenedParticles = GameObject.Instantiate(strengthenedParticlesPrefab, Vector3.zero, Quaternion.identity);
            strengthenedParticles.transform.position = Vector3.zero;
            strengthenedParticles.transform.SetParent(mob.transform, false);
            particlesActive = true;
        }

        //after effect duration finishes, reset
        if (Time.time > effectInitializedTime + effectDuration || complete)
        {
            complete = true;
            GameObject.Destroy(strengthenedParticles);
            mob.SetPhysicalAttackMultiplier(previousPhysicalAttackMultiplier);
            Debug.Log("I am no longer strengthened");
            effectApplied = false;
        }
    }
}
