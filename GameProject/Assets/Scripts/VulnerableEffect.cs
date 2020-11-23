using UnityEngine;
//This effect increases damage taken multipler
public class VulnerableEffect : EffectState
{
    float effectDuration;
    float previousDamageTakenMultiplier;
    //Damage taken multiplier increases by 50%
    float newDamageTakenMultiplier;

    bool effectApplied = false;

    bool particlesActive = false;
    GameObject vulnerableParticles;

    public VulnerableEffect(Mob mob, float effectDuration, float newDamageTakenMultiplier) : base(mob)
    {
        this.effectDuration = effectDuration;
        this.newDamageTakenMultiplier = newDamageTakenMultiplier;
        previousDamageTakenMultiplier = mob.physicalDamageTakenMultiplier;
    }

    protected override void Effect()
    {
        mob.SetPhysicalDamageTakenMultiplier(newDamageTakenMultiplier);
        Debug.Log("I am vulnerable!");
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
            var vulnerableParticlesPrefab = (GameObject)Resources.Load("prefabs/MobIsVulnerable", typeof(GameObject));
            vulnerableParticles = GameObject.Instantiate(vulnerableParticlesPrefab, Vector3.zero, Quaternion.identity);
            vulnerableParticles.transform.position = Vector3.zero;
            vulnerableParticles.transform.SetParent(mob.transform, false);
            particlesActive = true;
        }

        //after effect duration finishes, reset
        if (Time.time > effectInitializedTime + effectDuration || complete)
        {
            complete = true;
            GameObject.Destroy(vulnerableParticles);
            mob.SetPhysicalDamageTakenMultiplier(previousDamageTakenMultiplier);
            Debug.Log("I am no longer vulnerable");
            effectApplied = false;
        }
    }
}
