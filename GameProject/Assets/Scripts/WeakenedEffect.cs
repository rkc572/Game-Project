using UnityEngine;

//This effect lowers Physical Attack Multiplier
public class WeakenedEffect : EffectState
{
    
    float effectDuration;

    float previousDamageMultiplier;
    //Halves damage
    float newDamageMultiplier;

    bool effectApplied = false;

    bool particlesActive;
    GameObject weakenedParticles;

    public WeakenedEffect(Mob mob, float effectDuration, float newDamageMultiplier) : base(mob)
    {
        this.effectDuration = effectDuration;
        this.newDamageMultiplier = newDamageMultiplier;
        previousDamageMultiplier = mob.physicalAttackMultiplier;
    }

    protected override void Effect()
    {
        Debug.Log("I'm weakened!");
        mob.SetPhysicalAttackMultiplier(newDamageMultiplier);
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
            var weakenedParticlesPrefab = (GameObject)Resources.Load("prefabs/MobIsWeakened", typeof(GameObject));
            weakenedParticles = GameObject.Instantiate(weakenedParticlesPrefab, Vector3.zero, Quaternion.identity);
            weakenedParticles.transform.position = Vector3.zero;
            weakenedParticles.transform.SetParent(mob.transform, false);
            particlesActive = true;
        }

        //After effect duration finishes, reset
        if (Time.time > effectInitializedTime + effectDuration || complete)
        {
            complete = true;
            GameObject.Destroy(weakenedParticles);
            mob.SetPhysicalAttackMultiplier(previousDamageMultiplier);
            Debug.Log("I'm not weakened!");
            effectApplied = false;
        }
    }

   
}
